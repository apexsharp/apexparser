namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    /*
    MIT License

    Copyright (c) 2018 open-force

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    */
    /**
     * Utility class to streamline parsing nested JSON data structures.
     *
     * @see https://github.com/open-force/jsonparse
     */
    [WithSharing]
    public class JSONParse
    {
        private static readonly Pattern ARRAY_NOTATION = Pattern.compile("\\[(\\d+)\\]");

        /**
         * Every JSONParse instance is a wrapper around some actual data, which we store here.
         */
        private object value;

        /**
         * Create a JSONParse instance from a serialized JSON string.
         *
         * @param jsonData
         */
        public JSONParse(string jsonData)
        {
            value = JSON.deserializeUntyped(jsonData);
        }

        /**
         * Create a JSONParse instance from data that has already been unmarshalled from a JSON string.
         *
         * @param value
         */
        private JSONParse(object value)
        {
            this.value = value;
        }

        // ---------------------------------------
        // ------ Interactions -------------------
        // ---------------------------------------
        /**
         * Drill into a nested structure to get to some subtree in the document. We allow the path to include a mix
         * of array notation and string keys.
         *
         * @param path
         *
         * @return A new JSONParse wrapper that wraps the targeted subtree.
         * @throws NotAnArrayException if we try to apply an array notation to a node that isn't an array
         * @throws NotAnObjectException if we try to apply a map key to a node that isn't an object
         * @throws MissingKeyException if part of the path can't be resolved because there is no match on that key
         */
        public JSONParse get(string path)
        {
            JSONParse currentNode = this; // we start with ourselves and drill deeper

            // drill down through the nested structure
            foreach (string key in path.split("\\."))
            {
                // check to see if we are going to parse this key as a reference to an array item
                Matcher arrayMatcher = ARRAY_NOTATION.matcher(key);
                if (arrayMatcher.matches())
                {
                    int index = Integer.valueOf(arrayMatcher.group(1));
                    currentNode = currentNode.asList().get(index);
                }
                else
                {
                    // otherwise, treat this key as a normal map key
                    Map<string, JSONParse> wrappedMap = currentNode.asMap();
                    if (!wrappedMap.containsKey(key))
                    {
                        throw new MissingKeyException("No match found for <"+ key + ">: "+ wrappedMap.keySet());
                    }

                    currentNode = wrappedMap.get(key);
                }
            }

            return currentNode;
        }

        /**
         * Make an assumption that this JSONParse instance wraps a JSON object, and attempt to return a Map of the values.
         *
         * @return A Map of JSONParse instances
         * @throws NotAnObjectException if the internal wrapped value is not a JSON object
         */
        public Map<string, JSONParse> asMap()
        {
            if (!isObject())
            {
                throw new NotAnObjectException("The wrapped value is not a JSON object:\n"+ toStringPretty());
            }

            Map<string, object> valueAsMap = (Map<string, object>)value;
            Map<string, JSONParse> wrappers = new Map<string, JSONParse>();
            foreach (string key in valueAsMap.keySet())
            {
                wrappers.put(key, new JSONParse(valueAsMap.get(key)));
            }

            return wrappers;
        }

        /**
         * Make an assumption that this JSONParse instance wraps a List, and attempt to return an iterable version
         * of the values.
         *
         * @return A List of JSONParse instances, each wrapping one of the List items
         * @throws NotAnArrayException if the internal wrapped value is not a List instance
         */
        public List<JSONParse> asList()
        {
            if (!isArray())
            {
                throw new NotAnArrayException("The wrapped value is not a JSON array:\n"+ toStringPretty());
            }

            List<JSONParse> wrappers = new List<JSONParse>();
            foreach (object item in (List<object>)value)
            {
                wrappers.add(new JSONParse(item));
            }

            return wrappers;
        }

        // ---------------------------------------
        // ------ Utility ------------------------
        // ---------------------------------------
        public bool isObject()
        {
            return value is Map<string, object>;
        }

        public bool isArray()
        {
            return value is List<object>;
        }

        public string toStringPretty()
        {
            return JSON.serializePretty(value);
        }

        // ---------------------------------------
        // ------ Value Extraction ---------------
        // ---------------------------------------
        public Blob getBlobValue()
        {
            if (value is string)
                return EncodingUtil.base64Decode((string)value);
            throw new InvalidConversionException("Only String values can be converted to a Blob: "+ toStringPretty());
        }

        public bool getBooleanValue()
        {
            if (value is bool)
                return (bool)value;
            return Boolean.valueOf(value);
        }

        public Datetime getDatetimeValue()
        {
            if (value is long)
                return Datetime.newInstance((long)value);
            if (value is string)
                return Datetime.valueOfGmt(((string)value).replace("T", " "));
            throw new InvalidConversionException("Only Long and String values can be converted to a Datetime: "+ toStringPretty());
        }

        public Date getDateValue()
        {
            if (value is long)
                return Datetime.newInstance((long)value).dateGmt();
            if (value is string)
                return Date.valueOf((string)value);
            throw new InvalidConversionException("Only Long and String values can be converted to a Date: "+ toStringPretty());
        }

        public decimal getDecimalValue()
        {
            if (value is decimal)
                return (decimal)value;
            if (value is string)
                return Decimal.valueOf((string)value);
            throw new InvalidConversionException("This value cannot be converted to a Decimal: "+ toStringPretty());
        }

        public double getDoubleValue()
        {
            if (value is double)
                return (double)value;
            if (value is string)
                return Double.valueOf((string)value);
            throw new InvalidConversionException("This value cannot be converted to a Double: "+ toStringPretty());
        }

        public ID getIdValue()
        {
            if (value is string)
                return Id.valueOf((string)value);
            throw new InvalidConversionException("This value cannot be converted to an Id: "+ toStringPretty());
        }

        public int getIntegerValue()
        {
            if (value is int)
                return (int)value;
            if (value is decimal)
                return ((decimal)value).intValue();
            if (value is string)
                return Integer.valueOf((string)value);
            throw new InvalidConversionException("This value cannot be converted to an Integer: "+ toStringPretty());
        }

        public long getLongValue()
        {
            if (value is long)
                return (long)value;
            if (value is decimal)
                return ((decimal)value).longValue();
            if (value is string)
                return Long.valueOf((string)value);
            throw new InvalidConversionException("This value cannot be converted to a Long: "+ toStringPretty());
        }

        public string getStringValue()
        {
            if (isObject()|| isArray())
                throw new InvalidConversionException("Objects and arrays are not Strings: "+ toStringPretty());
            if (value is string)
                return (string)value;
            return value.ToString();
        }

        public Time getTimeValue()
        {
            if (value is long)
                return Datetime.newInstance((long)value).timeGmt();
            if (value is string)
                return Datetime.valueOfGmt(((string)value).replace("T", " ")).timeGmt();
            throw new InvalidConversionException("Only Long and String values can be converted to a Time: "+ toStringPretty());
        }

        public object getValue()
        {
            return value;
        }

        // ---------------------------------------
        // ------ Exceptions ---------------------
        // ---------------------------------------
        public class NotAnObjectException : Exception
        {
            [ApexSharpGenerated]
            public NotAnObjectException() : base()
            {
            }

            [ApexSharpGenerated]
            public NotAnObjectException(string message) : base(message)
            {
            }

            [ApexSharpGenerated]
            public NotAnObjectException(Exception e) : base(e)
            {
            }

            [ApexSharpGenerated]
            public NotAnObjectException(string message, Exception e) : base(message, e)
            {
            }
        }

        public class NotAnArrayException : Exception
        {
            [ApexSharpGenerated]
            public NotAnArrayException() : base()
            {
            }

            [ApexSharpGenerated]
            public NotAnArrayException(string message) : base(message)
            {
            }

            [ApexSharpGenerated]
            public NotAnArrayException(Exception e) : base(e)
            {
            }

            [ApexSharpGenerated]
            public NotAnArrayException(string message, Exception e) : base(message, e)
            {
            }
        }

        public class MissingKeyException : Exception
        {
            [ApexSharpGenerated]
            public MissingKeyException() : base()
            {
            }

            [ApexSharpGenerated]
            public MissingKeyException(string message) : base(message)
            {
            }

            [ApexSharpGenerated]
            public MissingKeyException(Exception e) : base(e)
            {
            }

            [ApexSharpGenerated]
            public MissingKeyException(string message, Exception e) : base(message, e)
            {
            }
        }

        public class InvalidConversionException : Exception
        {
            [ApexSharpGenerated]
            public InvalidConversionException() : base()
            {
            }

            [ApexSharpGenerated]
            public InvalidConversionException(string message) : base(message)
            {
            }

            [ApexSharpGenerated]
            public InvalidConversionException(Exception e) : base(e)
            {
            }

            [ApexSharpGenerated]
            public InvalidConversionException(string message, Exception e) : base(message, e)
            {
            }
        }
    }
}
