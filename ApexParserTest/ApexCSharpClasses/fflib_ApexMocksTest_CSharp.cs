namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.NUnit;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;

    /*
     * Copyright (c) 2014-2017 FinancialForce.com, inc.  All rights reserved.
     */
    [TestFixture]
    private class fflib_ApexMocksTest
    {
        private static readonly fflib_ApexMocks MY_MOCKS = new fflib_ApexMocks();

        private static readonly fflib_MyList MY_MOCK_LIST = (fflib_MyList)MY_MOCKS.mock(typeof(fflib_MyList));

        [Test]
        static void whenStubMultipleCallsWithMatchersShouldReturnExpectedValues()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(fflib_Match.anyInteger(), fflib_Match.anyString())).thenReturn("any");
            mocks.when(mockList.get2(fflib_Match.anyInteger(), fflib_Match.stringContains("Hello"))).thenReturn("hello");
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            System.assertEquals("hello", actualValue);
        }

        [Test]
        static void whenVerifyMultipleCallsWithMatchersShouldReturnCorrectMethodCallCounts()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");
            mockList.add("fred");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add(fflib_Match.anyString());
            ((fflib_MyList.IList)mocks.verify(mockList)).add("fred");
            ((fflib_MyList.IList)mocks.verify(mockList)).add(fflib_Match.stringContains("fred"));
        }

        [Test]
        static void whenStubExceptionWithMatchersShouldThrowException()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList.IList)mocks.doThrowWhen(new MyException("Matcher Exception"),  mockList)).add(fflib_Match.stringContains("Hello"));
            mocks.stopStubbing();

            // When
            mockList.add("Hi");
            try
            {
                mockList.add("Hi Hello Hi");
                System.assert(false, "Expected exception");
            }
            catch (MyException e)
            {
                //Then
                System.assertEquals("Matcher Exception", e.getMessage());
            }
        }

        [Test]
        static void whenVerifyWithCombinedMatchersShouldReturnCorrectMethodCallCounts()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");
            mockList.add("fred");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, 0)).add((string)fflib_Match.allOf(fflib_Match.eq("bob"), fflib_Match.stringContains("re")));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)fflib_Match.allOf(fflib_Match.eq("fred"), fflib_Match.stringContains("re")));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)fflib_Match.anyOf(fflib_Match.eq("bob"), fflib_Match.eq("fred")));
            ((fflib_MyList.IList)mocks.verify(mockList, 1)).add((string)fflib_Match.anyOf(fflib_Match.eq("bob"), fflib_Match.eq("jack")));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)fflib_Match.noneOf(fflib_Match.eq("jack"), fflib_Match.eq("tim")));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)fflib_Match.noneOf(fflib_Match.anyOf(fflib_Match.eq("jack"), fflib_Match.eq("jill")),
				fflib_Match.allOf(fflib_Match.eq("tim"), fflib_Match.stringContains("i"))));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)fflib_Match.isNot(fflib_Match.eq("jack")));
        }

        [Test]
        static void whenStubCustomMatchersCanBeUsed()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get((int)fflib_Match.matches(new isOdd()))).thenReturn("Odd");
            mocks.when(mockList.get((int)fflib_Match.matches(new isEven()))).thenReturn("Even");
            mocks.stopStubbing();

            // When
            string s1 = mockList.get(1);
            string s2 = mockList.get(2);
            string s3 = mockList.get(3);
            string s4 = mockList.get(4);
            string s5 = mockList.get(5);

            // Then
            System.assertEquals("Odd", s1);
            System.assertEquals("Even", s2);
            System.assertEquals("Odd", s3);
            System.assertEquals("Even", s4);
            System.assertEquals("Odd", s5);
        }

        [Test]
        static void whenVerifyCustomMatchersCanBeUsed()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.get(1);
            mockList.get(2);
            mockList.get(3);
            mockList.get(4);
            mockList.get(5);

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, 3)).get((int)fflib_Match.matches(new isOdd()));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).get((int)fflib_Match.matches(new isEven()));
        }

        [Test]
        static void whenStubWithMatcherAndNonMatcherArgumentsShouldThrowException()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            string expectedError = "The number of matchers defined (1)."+ " does not match the number expected (2)\n"+ "If you are using matchers all arguments must be passed in as matchers.\n"+ "For example myList.add(fflib_Match.anyInteger(), \'String\') should be defined as myList.add(fflib_Match.anyInteger(), fflib_Match.eq(\'String\')).";

            // Then
            try
            {
                mocks.startStubbing();
                mocks.when(mockList.get2(fflib_Match.anyInteger(), "String literal")).thenReturn("fail");
                System.assert(false, "Expected exception");
            }
            catch (fflib_ApexMocks.ApexMocksException e)
            {
                System.assertEquals(expectedError, e.getMessage());
            }
        }

        [Test]
        static void whenVerifyWithMatcherAndNonMatcherArgumentsShouldThrowException()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            string expectedError = "The number of matchers defined (1)."+ " does not match the number expected (2)\n"+ "If you are using matchers all arguments must be passed in as matchers.\n"+ "For example myList.add(fflib_Match.anyInteger(), \'String\') should be defined as myList.add(fflib_Match.anyInteger(), fflib_Match.eq(\'String\')).";
            mockList.get2(1, "String literal");

            // Then
            try
            {
                ((fflib_MyList.IList)mocks.verify(mockList)).get2(fflib_Match.anyInteger(), "String literal");
                System.assert(false, "Expected exception");
            }
            catch (fflib_ApexMocks.ApexMocksException e)
            {
                System.assertEquals(expectedError, e.getMessage());
            }
        }

        [Test]
        static void whenStubSameMethodWithMatchersAndNonMatchersShouldStubInOrder()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(1, "Non-matcher first")).thenReturn("Bad"); //Set the return value using the non-matcher arguments
            mocks.when(mockList.get2(fflib_Match.eqInteger(1), fflib_Match.stringContains("Non-matcher first"))).thenReturn("Good"); //Override the return value using matcher arguments
            mocks.when(mockList.get2(fflib_Match.eqInteger(1), fflib_Match.stringContains("Matcher first"))).thenReturn("Bad"); //Set the return value using the matcher arguments
            mocks.when(mockList.get2(1, "Matcher first")).thenReturn("Good"); //Override the return value using non-matcher arguments
            mocks.stopStubbing();

            // When/Thens
            System.assertEquals("Good", mockList.get2(1, "Non-matcher first"));
            System.assertEquals("Good", mockList.get2(1, "Matcher first"));
        }

        [Test]
        static void whenStubExceptionSameMethodWithMatchersAndNonMatchersShouldStubInOrder()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList.IList)mocks.doThrowWhen(new fflib_ApexMocks.ApexMocksException("Bad"), mockList)).add("Non-matcher first"); //Set the exception value using the non-matcher arguments
            ((fflib_MyList.IList)mocks.doThrowWhen(new fflib_ApexMocks.ApexMocksException("Good"), mockList)).add(fflib_Match.stringContains("Non-matcher first")); //Override the exception value using matcher arguments
            ((fflib_MyList.IList)mocks.doThrowWhen(new fflib_ApexMocks.ApexMocksException("Bad"), mockList)).add(fflib_Match.stringContains("Matcher first")); //Set the exception value using the matcher arguments
            ((fflib_MyList.IList)mocks.doThrowWhen(new fflib_ApexMocks.ApexMocksException("Good"), mockList)).add("Matcher first"); //Override the exception value using non-matcher arguments
            mocks.stopStubbing();

            // When/Thens
            try
            {
                mockList.add("Non-matcher first");
                System.assert(false, "Expected exception");
            }
            catch (fflib_ApexMocks.ApexMocksException e)
            {
                System.assertEquals("Good", e.getMessage());
            }

            try
            {
                mockList.add("Matcher first");
                System.assert(false, "Expected exception");
            }
            catch (fflib_ApexMocks.ApexMocksException e)
            {
                System.assertEquals("Good", e.getMessage());
            }
        }

        [Test]
        static void whenStubSingleCallWithSingleArgumentShouldReturnStubbedValue()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(0)).thenReturn("bob");
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get(0);

            // Then
            System.assertEquals("bob", actualValue);
        }

        [Test]
        static void whenStubSingleCallWithNullReturnValueItShouldReturnNull()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(0)).thenReturn(null);
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get(0);

            // Then
            System.assertEquals(null, actualValue);
        }

        [Test]
        static void whenStubMultipleCallsWithSingleArgumentShouldReturnStubbedValues()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(0)).thenReturn("bob");
            mocks.when(mockList.get(1)).thenReturn("fred");
            mocks.stopStubbing();

            // When
            string actualValueArg0 = mockList.get(0);
            string actualValueArg1 = mockList.get(1);
            string actualValueArg2 = mockList.get(2);

            // Then
            System.assertEquals("bob", actualValueArg0);
            System.assertEquals("fred", actualValueArg1);
            System.assertEquals(null, actualValueArg2);
        }

        [Test]
        static void whenStubSameCallWithDifferentArgumentValueShouldReturnLastStubbedValue()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(0)).thenReturn("bob1");
            mocks.when(mockList.get(0)).thenReturn("bob2");
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get(0);

            // Then
            System.assertEquals("bob2", actualValue);
        }

        [Test]
        static void whenStubCallWithNoArgumentsShouldReturnStubbedValue()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.isEmpty()).thenReturn(false);
            mocks.stopStubbing();

            // When
            bool actualValue = mockList.isEmpty();

            // Then
            System.assertEquals(false, actualValue);
        }

        [Test]
        static void verifySingleMethodCallWithNoArguments()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.isEmpty();

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList)).isEmpty();
        }

        [Test]
        static void verifySingleMethodCallWithSingleArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList)).add("bob");
        }

        [Test]
        static void verifyMultipleMethodCallsWithSameSingleArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");
            mockList.add("bob");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add("bob");
        }

        [Test]
        static void verifyMultipleMethodCallsWithDifferentSingleArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");
            mockList.add("fred");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList)).add("bob");
            ((fflib_MyList.IList)mocks.verify(mockList)).add("fred");
        }

        [Test]
        static void verifyMethodCallsWithSameNameButDifferentArgumentTypes()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");
            mockList.add(new string[]{"bob"});
            mockList.add((string)null);
            mockList.add((string[])null);

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList)).add("bob");
            ((fflib_MyList.IList)mocks.verify(mockList)).add(new string[]{"bob"});
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)null);
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string[])null);
        }

        [Test]
        static void verifyMethodNotCalled()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.get(0);

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).add("bob");
            ((fflib_MyList.IList)mocks.verify(mockList)).get(0);
        }

        [Test]
        static void stubAndVerifyMethodCallsWithNoArguments()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.isEmpty()).thenReturn(false);
            mocks.stopStubbing();
            mockList.clear();

            // When
            bool actualValue = mockList.isEmpty();

            // Then
            System.assertEquals(false, actualValue);
            ((fflib_MyList.IList)mocks.verify(mockList)).clear();
        }

        [Test]
        static void whenStubExceptionTheExceptionShouldBeThrown()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(0)).thenThrow(new MyException("Stubbed exception."));
            mocks.stopStubbing();

            // When
            try
            {
                mockList.get(0);
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("Stubbed exception.", e.getMessage());
            }
        }

        [Test]
        static void whenStubVoidMethodWithExceptionThenExceptionShouldBeThrown()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList.IList)mocks.doThrowWhen(new MyException("Stubbed exception."), mockList)).clear();
            mocks.stopStubbing();

            // When
            try
            {
                mockList.clear();
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("Stubbed exception.", e.getMessage());
            }
        }

        [Test]
        static void whenStubMultipleVoidMethodsWithExceptionsThenExceptionsShouldBeThrown()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList.IList)mocks.doThrowWhen(new MyException("clear stubbed exception."), mockList)).clear();
            ((fflib_MyList.IList)mocks.doThrowWhen(new MyException("add stubbed exception."), mockList)).add("bob");
            mocks.stopStubbing();

            // When
            try
            {
                mockList.clear();
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("clear stubbed exception.", e.getMessage());
            }

            // When
            try
            {
                mockList.add("bob");
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("add stubbed exception.", e.getMessage());
            }
        }

        [Test]
        static void whenStubVoidMethodWithExceptionAndCallMethodTwiceThenExceptionShouldBeThrownTwice()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList.IList)mocks.doThrowWhen(new MyException("clear stubbed exception."), mockList)).clear();
            mocks.stopStubbing();

            // When
            try
            {
                mockList.clear();
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("clear stubbed exception.", e.getMessage());
            }

            // When
            try
            {
                mockList.clear();
                System.assert(false, "Stubbed exception should have been thrown.");
            }
            catch (Exception e)
            {
                // Then
                System.assert(e is MyException);
                System.assertEquals("clear stubbed exception.", e.getMessage());
            }
        }

        [Test]
        static void verifyMethodCallWhenNoCallsBeenMadeForType()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).add("bob");
        }

        [Test]
        static void verifySingleMethodCallWithMultipleArguments()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.set(0, "bob");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList)).set(0, "bob");
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).set(0, "fred");
        }

        [Test]
        static void whenStubMultipleCallsWithMultipleArgumentShouldReturnStubbedValues()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "zero")).thenReturn("bob");
            mocks.when(mockList.get2(1, "one")).thenReturn("fred");
            mocks.when(mockList.get2(0, "two")).thenReturn("bob");
            mocks.when(mockList.get2(1, "three")).thenReturn("bub");
            mocks.stopStubbing();

            // When
            string actualValueArg0 = mockList.get2(0, "zero");
            string actualValueArg1 = mockList.get2(1, "one");
            string actualValueArg2 = mockList.get2(0, "two");
            string actualValueArg3 = mockList.get2(1, "three");
            string actualValueArg4 = mockList.get2(0, "three");

            // Then
            System.assertEquals("bob", actualValueArg0);
            System.assertEquals("fred", actualValueArg1);
            System.assertEquals("bob", actualValueArg2);
            System.assertEquals("bub", actualValueArg3);
            System.assertEquals(null, actualValueArg4);
        }

        [Test]
        static void whenStubNullConcreteArgValueCorrectValueIsReturned()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            string expected = "hello";
            mocks.startStubbing();
            mocks.when(mockList.get(null)).thenReturn(expected);
            mocks.stopStubbing();

            // When
            string actual = mockList.get(null);

            // Then
            System.assertEquals(expected, actual);
        }

        [Test]
        static void whenSetDoThrowWhenExceptionsValuesAreSet()
        {
            //Given
            MyException e = new MyException("Test");
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            List<Exception> expsList = new List<Exception>{e};

            //When
            mocks.DoThrowWhenExceptions = expsList;

            //Then
            System.assert(expsList === mocks.DoThrowWhenExceptions);
        }

        [Test]
        static void whenVerifyMethodNeverCalledMatchersAreReset()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("bob");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).get(fflib_Match.anyInteger());
            ((fflib_MyList.IList)mocks.verify(mockList)).add(fflib_Match.anyString());
        }

        [Test]
        static void whenMockIsGeneratedCanVerify()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList.IList mockList = new fflib_Mocks.Mockfflib_MyList(mocks);

            // When
            mockList.add("bob");

            // Then
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).get(fflib_Match.anyInteger());
            ((fflib_MyList.IList)mocks.verify(mockList)).add("bob");
        }

        [Test]
        static void whenMockIsGeneratedCanStubVerify()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList.IList mockList = new fflib_Mocks.Mockfflib_MyList(mocks);

            // When
            mocks.startStubbing();
            mocks.when(mockList.get(1)).thenReturn("One");
            mocks.when(mockList.get(fflib_Match.integerMoreThan(2))).thenReturn(">Two");
            mocks.stopStubbing();

            // Then
            System.assertEquals(null, mockList.get(0));
            System.assertEquals("One", mockList.get(1));
            System.assertEquals(null, mockList.get(2));
            System.assertEquals(">Two", mockList.get(3));
        }

        [Test]
        static void thatMultipleInstancesCanBeMockedIndependently()
        {
            fflib_ApexMocksConfig.HasIndependentMocks = true;

            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList first = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_MyList second = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(first.get(0)).thenReturn("First");
            mocks.when(second.get(0)).thenReturn("Second");
            mocks.stopStubbing();

            // When
            string actual = first.get(0);

            // Then
            System.assertEquals("First", actual, "Should have returned stubbed value");
            ((fflib_MyList)mocks.verify(first)).get(0);
            ((fflib_MyList)mocks.verify(second, mocks.never())).get(0);
        }

        [Test]
        static void thatMultipleInstancesCanBeMockedDependently()
        {
            fflib_ApexMocksConfig.HasIndependentMocks = false;

            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList first = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_MyList second = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(first.get(0)).thenReturn("First");
            mocks.when(second.get(0)).thenReturn("Second");
            mocks.stopStubbing();

            // When
            string actual = first.get(0);

            // Then
            System.assertEquals("Second", actual, "Should have returned stubbed value");
            ((fflib_MyList)mocks.verify(first)).get(0);
            ((fflib_MyList)mocks.verify(second)).get(0);
        }

        static void thatStubbingCanBeChainedFirstExceptionThenValue()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(new MyException("Stubbed exception.")).thenReturn("One");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("Stubbed exception.");
            assertReturnedValue("One");
        }

        [Test]
        static void thatStubbingCanBeChainedFirstValueThenException()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("One").thenThrow(new MyException("Stubbed exception."));
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertExceptionMessage("Stubbed exception.");
        }

        [Test]
        static void thatStubbingMultipleMethodsCanBeChainedFirstExceptionThenValue()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(new MyException("Stubbed exception.")).thenReturn("One");
            MY_MOCKS.when(MY_MOCK_LIST.get2(2, "Hello.")).thenThrow(new MyException("Stubbed exception2.")).thenReturn("One2");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("Stubbed exception.");
            assertReturnedValue("One");
            assertExceptionMessageForGet2("Stubbed exception2.");
            assertReturnedValueForGet2("One2");
        }

        [Test]
        static void thatStubbingMultipleMethodsCanBeChainedFirstValueThenException()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("One").thenThrow(new MyException("Stubbed exception."));
            MY_MOCKS.when(MY_MOCK_LIST.get2(2, "Hello.")).thenReturn("One2").thenThrow(new MyException("Stubbed exception2."));
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertExceptionMessage("Stubbed exception.");
            assertReturnedValueForGet2("One2");
            assertExceptionMessageForGet2("Stubbed exception2.");
        }

        [Test]
        static void thatStubbingReturnsDifferentValuesForDifferentCalls()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
        }

        [Test]
        static void thatStubbingReturnsDifferentValuesForDifferentCallsAndRepeatLastValuesForFurtherCalls()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
        }

        [Test]
        static void thatStubbingThrowsDifferentExceptionsForDifferentCalls()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingThrowsDifferentExceptionsForDifferentCallsAndRepeatLastExceptionForFurtherCalls()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingThrowsAndReturnsDifferentExceptionsAndValuesForDifferentCalls()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).
			thenThrowMulti(new List<Exception>{first, second, third}).
			thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
        }

        [Test]
        static void thatStubbingReturnsAndThrowsDifferentValuesAndExceptionsForDifferentCalls()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).
			thenReturnMulti(new List<string>{"One", "Two", "Three"}).
			thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnWithSingleValue()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("One");
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Two");
            assertReturnedValue("Two");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnMultiWithSingleValue()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Two");
            assertReturnedValue("Two");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnMultiWithMultiValue()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"Four", "Five", "Six"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Four");
            assertReturnedValue("Five");
            assertReturnedValue("Six");
            assertReturnedValue("Six");
            assertReturnedValue("Six");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnWithMultiValues()
        {
            // Given
            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnMultiWithSingleException()
        {
            // Given
            MyException first = new MyException("first.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(first);
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("first.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnMultiWithMultiExceptions()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnWithMultiExceptions()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenReturnWithSingleException()
        {
            // Given
            MyException first = new MyException("first.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(first);
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("first.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowWithSingleValue()
        {
            // Given
            MyException first = new MyException("first.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(first);
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Two");
            assertReturnedValue("Two");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowMultiWithSingleValue()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn("Two");
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Two");
            assertReturnedValue("Two");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowMultiWithMultiValue()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"Four", "Five", "Six"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("Four");
            assertReturnedValue("Five");
            assertReturnedValue("Six");
            assertReturnedValue("Six");
            assertReturnedValue("Six");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowWithMultiValues()
        {
            // Given
            MyException first = new MyException("first.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(first);
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<string>{"One", "Two", "Three"});
            MY_MOCKS.stopStubbing();

            // Then
            assertReturnedValue("One");
            assertReturnedValue("Two");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
            assertReturnedValue("Three");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowMultiWithSingleException()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");
            MyException fourth = new MyException("fourth.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(fourth);
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("fourth.");
            assertExceptionMessage("fourth.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowMultiWithMultiExceptions()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");
            MyException fourth = new MyException("fourth.");
            MyException fifth = new MyException("fifth.");
            MyException sixth = new MyException("sixth.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{fourth, fifth, sixth});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("fourth.");
            assertExceptionMessage("fifth.");
            assertExceptionMessage("sixth.");
            assertExceptionMessage("sixth.");
            assertExceptionMessage("sixth.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowWithMultiExceptions()
        {
            // Given
            MyException beforeFirst = new MyException("before first.");
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(beforeFirst);
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>{first, second, third});
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("second.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
            assertExceptionMessage("third.");
        }

        [Test]
        static void thatStubbingMultipleTimesOverridePreviousThenThrowWithSingleException()
        {
            // Given
            MyException beforeFirst = new MyException("before first.");
            MyException first = new MyException("first.");

            // When
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(beforeFirst);
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(first);
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessage("first.");
            assertExceptionMessage("first.");
        }

        [Test]
        static void thatVoidMethodThrowsMultipleExceptions()
        {
            // Given
            MyException beforeFirst = new MyException("before first.");
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");

            // When
            MY_MOCKS.startStubbing();
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{first, second, third},  MY_MOCK_LIST)).add("Hello");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessageOnVoidMethod("first.");
            assertExceptionMessageOnVoidMethod("second.");
            assertExceptionMessageOnVoidMethod("third.");
            assertExceptionMessageOnVoidMethod("third.");
            assertExceptionMessageOnVoidMethod("third.");
        }

        [Test]
        static void thatMultipleVoidMethodsThrowsMultipleExceptions()
        {
            // Given
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");
            MyException first2 = new MyException("first2.");
            MyException second2 = new MyException("second2.");
            MyException third2 = new MyException("third2.");

            // When
            MY_MOCKS.startStubbing();
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{first2, second2, third2},  MY_MOCK_LIST)).addMore("Hello");
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{first, second, third},  MY_MOCK_LIST)).add("Hello");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessageOnVoidMethod("first.");
            assertExceptionMessageOnVoidMethod("second.");
            assertExceptionMessageOnVoidMethod("third.");
            assertExceptionMessageOnVoidMethod("third.");
            assertExceptionMessageOnVoidMethod("third.");
            assertExceptionMessageOnAddMoreVoidMethod("first2.");
            assertExceptionMessageOnAddMoreVoidMethod("second2.");
            assertExceptionMessageOnAddMoreVoidMethod("third2.");
            assertExceptionMessageOnAddMoreVoidMethod("third2.");
            assertExceptionMessageOnAddMoreVoidMethod("third2.");
        }

        [Test]
        static void thatStubbingMutipleTimesVoidMethodThrowsMultipleExceptionsOverride()
        {
            // Given
            MyException beforeFirst = new MyException("before first.");
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");
            MyException fourth = new MyException("fourth.");
            MyException fifth = new MyException("fifth.");
            MyException sixth = new MyException("sixth.");

            // When
            MY_MOCKS.startStubbing();
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{first, second, third},  MY_MOCK_LIST)).add("Hello");
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{fourth, fifth, sixth},  MY_MOCK_LIST)).add("Hello");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessageOnVoidMethod("fourth.");
            assertExceptionMessageOnVoidMethod("fifth.");
            assertExceptionMessageOnVoidMethod("sixth.");
            assertExceptionMessageOnVoidMethod("sixth.");
            assertExceptionMessageOnVoidMethod("sixth.");
        }

        [Test]
        static void thatStubbingMutipleTimesVoidMethodThrowsMultipleExceptionsOverrideWithSingleException()
        {
            // Given
            MyException beforeFirst = new MyException("before first.");
            MyException first = new MyException("first.");
            MyException second = new MyException("second.");
            MyException third = new MyException("third.");
            MyException fourth = new MyException("fourth.");
            MyException fifth = new MyException("fifth.");
            MyException sixth = new MyException("sixth.");

            // When
            MY_MOCKS.startStubbing();
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(new List<Exception>{first, second, third},  MY_MOCK_LIST)).add("Hello");
            ((fflib_MyList.IList)MY_MOCKS.doThrowWhen(fourth,  MY_MOCK_LIST)).add("Hello");
            MY_MOCKS.stopStubbing();

            // Then
            assertExceptionMessageOnVoidMethod("fourth.");
            assertExceptionMessageOnVoidMethod("fourth.");
        }

        [Test]
        static void thatExceptionIsthrownWhenStubbingIsNotDone()
        {
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1));
            MY_MOCKS.stopStubbing();
            try
            {
                MY_MOCK_LIST.get(1);
                System.assert(false, "an exception was expected");
            }
            catch (fflib_ApexMocks.ApexMocksException myex)
            {
                System.assertEquals("The stubbing is not correct, no return values have been set.",
				myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        [Test]
        static void thatExceptionIsthrownWhenReturnMultiPassEmptyList()
        {
            try
            {
                MY_MOCKS.startStubbing();
                MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(new List<object>());
                MY_MOCKS.stopStubbing();
                System.assert(false, "an exception was expected");
            }
            catch (fflib_ApexMocks.ApexMocksException myex)
            {
                System.assertEquals("The stubbing is not correct, no return values have been set.",
				myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        [Test]
        static void thatExceptionIsthrownWhenReturnMultiPassNullList()
        {
            try
            {
                MY_MOCKS.startStubbing();
                MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturnMulti(null);
                MY_MOCKS.stopStubbing();
                System.assert(false, "an exception was expected");
            }
            catch (fflib_ApexMocks.ApexMocksException myex)
            {
                System.assertEquals("The stubbing is not correct, no return values have been set.",
				myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        [Test]
        static void thatExceptionIsthrownWhenThrowMultiPassEmptyList()
        {
            try
            {
                MY_MOCKS.startStubbing();
                MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(new List<Exception>());
                MY_MOCKS.stopStubbing();
                System.assert(false, "an exception was expected");
            }
            catch (fflib_ApexMocks.ApexMocksException myex)
            {
                System.assertEquals("The stubbing is not correct, no return values have been set.",
				myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        [Test]
        static void thatExceptionIsthrownWhenThrowMultiPassNullList()
        {
            try
            {
                MY_MOCKS.startStubbing();
                MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrowMulti(null);
                MY_MOCKS.stopStubbing();
                System.assert(false, "an exception was expected");
            }
            catch (fflib_ApexMocks.ApexMocksException myex)
            {
                System.assertEquals("The stubbing is not correct, no return values have been set.",
				myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        [Test]
        static void thatNullCanBeUsedAsReturnValue()
        {
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenReturn(null);
            MY_MOCKS.stopStubbing();
            System.assertEquals(null, MY_MOCK_LIST.get(1), "it should be possible stub using the null value");
        }

        [Test]
        static void thatNullCanBeUsedAsExceptionvalue()
        {
            MY_MOCKS.startStubbing();
            MY_MOCKS.when(MY_MOCK_LIST.get(1)).thenThrow(null);
            MY_MOCKS.stopStubbing();
            System.assertEquals(null, MY_MOCK_LIST.get(1), "it should be possible stub using the null value");
        }

        private static void assertExceptionMessage(string expectedMessage)
        {
            try
            {
                MY_MOCK_LIST.get(1);
                System.assert(false, "an exception was expected");
            }
            catch (MyException myex)
            {
                System.assertEquals(expectedMessage, myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        private static void assertExceptionMessageForGet2(string expectedMessage)
        {
            try
            {
                MY_MOCK_LIST.get2(2, "Hello.");
                System.assert(false, "an exception was expected");
            }
            catch (MyException myex)
            {
                System.assertEquals(expectedMessage, myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        private static void assertExceptionMessageOnVoidMethod(string expectedMessage)
        {
            try
            {
                MY_MOCK_LIST.add("Hello");
                System.assert(false, "an exception was expected");
            }
            catch (MyException myex)
            {
                System.assertEquals(expectedMessage, myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        private static void assertExceptionMessageOnAddMoreVoidMethod(string expectedMessage)
        {
            try
            {
                MY_MOCK_LIST.addMore("Hello");
                System.assert(false, "an exception was expected");
            }
            catch (MyException myex)
            {
                System.assertEquals(expectedMessage, myex.getMessage(), "the message reported by the exception is not correct");
            }
        }

        private static void assertReturnedValue(string expectedValue)
        {
            System.assertEquals(expectedValue, MY_MOCK_LIST.get(1), "the method did not returned the expected value");
        }

        private static void assertReturnedValueForGet2(string expectedValue)
        {
            System.assertEquals(expectedValue, MY_MOCK_LIST.get2(2, "Hello."), "the method did not returned the expected value");
        }

        private class MyException : Exception
        {
        }

        private class isOdd : fflib_IMatcher
        {
            public bool matches(object arg)
            {
                return arg is int ? Math.mod((int)arg, 2)== 1: false;
            }
        }

        private class isEven : fflib_IMatcher
        {
            public bool matches(object arg)
            {
                return arg is int ? Math.mod((int)arg, 2)== 0: false;
            }
        }
    }
}
