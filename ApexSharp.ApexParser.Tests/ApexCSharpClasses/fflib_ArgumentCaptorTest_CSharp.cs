namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.ApexSharp.NUnit;
    using Apex.System;
    using SObjects;

    /*
     * Copyright (c) 2016-2017 FinancialForce.com, inc.  All rights reserved.
     */
    /**
     * @nodoc
     */
    [TestFixture]
    private class fflib_ArgumentCaptorTest
    {
        [Test]
        static void thatArgumentValueIsCaptured()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
        }

        [Test]
        static void thatCanPerformFurtherAssertionsOnCapturedArgumentValue()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            //When
            TestInnerClass testValue = new TestInnerClass();
            testValue.i = 4;
            testValue.s = "5";
            mockList.set(1, testValue);

            //Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(TestInnerClass));
            ((fflib_MyList.IList)mocks.verify(mockList)).set(fflib_Match.anyInteger(),  argument.capture());
            object capturedArg = argument.getValue();
            System.assertNotEquals(null, capturedArg, "CapturedArg should not be null");
            System.assert(capturedArg is TestInnerClass, "CapturedArg should be SObject, instead was "+ capturedArg);
            TestInnerClass testValueCaptured = (TestInnerClass)capturedArg;
            System.assertEquals(4, testValueCaptured.i, "the values inside the argument captured should be the same of the original one");
            System.assertEquals("5", testValueCaptured.s, "the values inside the argument captured should be the same of the original one");
        }

        [Test]
        static void thatCaptureArgumentOnlyFromVerifiedMethod()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred");

            //the next call should be ignored because is not the method that has under verify,
            //even if have the same type specified in the capturer.
            mockList.addMore("Barney");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
            System.assertEquals(1, argument.getAllValues().size(), "the argument captured should be only one");
        }

        [Test]
        static void thatCaptureAllArgumentsForTheVerifiedMethods()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            List<string> stringList = new List<string>{"3"};

            // When
            mockList.add("Fred");
            mockList.add(stringList);
            mockList.clear();

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)argument.capture());
            ((fflib_MyList.IList)mocks.verify(mockList)).add((List<string>)argument.capture());
            System.assertEquals(stringList, (List<string>)argument.getValue(), "the argument captured is not as expected");
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(2, argsCaptured.size(), "expected 2 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
        }

        [Test]
        static void thatCaptureArgumentFromRequestedParameter()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred", "Barney", "Wilma", "Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)fflib_Match.eq("Fred"),
				(string)fflib_Match.eq("Barney"),
				(string)argument.capture(),
				(string)fflib_Match.eq("Betty"));
            System.assertEquals("Wilma", (string)argument.getValue(),
			"the argument captured is not as expected, should be Wilma because is the 3rd parameter in the call");
        }

        [Test]
        static void thatCaptureLastArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Barney");
            mockList.add("Fred");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
        }

        [Test]
        static void thatCaptureAllArguments()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred");
            mockList.add("Barney");
            mockList.add("Wilma");
            mockList.add("Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList, 4)).add((string)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(4, argsCaptured.size(), "expected 4 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[1], "the second value is not as expected");
            System.assertEquals("Wilma", (string)argsCaptured[2], "the third value is not as expected");
            System.assertEquals("Betty", (string)argsCaptured[3], "the forth value is not as expected");
        }

        [Test]
        static void thatCaptureAllArgumentsFromMultipleMethods()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred");
            mockList.add("Barney");
            mockList.get2(3, "pebble");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList, 2)).add((string)argument.capture());
            ((fflib_MyList.IList)mocks.verify(mockList)).get2((int)fflib_Match.eq(3),
				(string)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(3, argsCaptured.size(), "expected 3 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[1], "the second value is not as expected");
            System.assertEquals("pebble", (string)argsCaptured[2], "the third value is not as expected");
        }

        [Test]
        static void thatCanHandleMultipleCapturesInOneMethodCall()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred", "Barney", "Wilma", "Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)fflib_Match.eq("Fred"),
				(string)argument.capture(),
				(string)argument.capture(),
				(string)fflib_Match.eq("Betty"));
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(2, argsCaptured.size(), "expected 2 argument to be captured");
            System.assertEquals("Barney", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Wilma", (string)argsCaptured[1], "the second value is not as expected");
        }

        [Test]
        static void thatDoesNotCaptureIfNotVerified()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("3");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(List<string>));
            ((fflib_MyList.IList)mocks.verify(mockList, fflib_ApexMocks.NEVER)).add((List<string>)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(0, argsCaptured.size(), "expected 0 argument to be captured");
            System.assertEquals(null, argument.getValue(), "no value should be captured, so must return null");
        }

        [Test]
        static void thatCaptureOnlyMethodsThatMatchesWithOtherMatcherAsWell()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Same", "Same", "First call", "First call");
            mockList.add("Same", "Same", "Second call", "Second call");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add(fflib_Match.eqString("Same"),
			fflib_Match.eqString("Same"),
			(string)argument.capture(),
			fflib_Match.eqString("First call"));
            System.assertEquals("First call", (string)argument.getValue());
        }

        [Test]
        static void thatDoesNotCaptureAnythingWhenCaptorIsWrappedInAMatcher()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Same", "Same", "First call", "First call");
            mockList.add("Same", "Same", "Second call", "Second call");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList)).add((string)fflib_Match.allOf(fflib_Match.eqString("Same"),
				fflib_Match.eqString("Same"),
				argument.capture()),
			(string)fflib_Match.allOf(fflib_Match.eqString("Same"),
				fflib_Match.eqString("Same"),
				argument.capture()),
			(string)fflib_Match.allOf(argument.capture(),
				fflib_Match.eqString("First call")),
			(string)fflib_Match.allOf(argument.capture(),
				fflib_Match.eqString("First call")));
            List<object> capturedValues = argument.getAllValues();
            System.assertEquals(0, capturedValues.size(),
			"nothing should have been capture because the matcher it not really a capture type, but a allOf()");
            System.assertEquals(null, (string)argument.getValue(),
			"nothing should have been capture because the matcher it not really a capture type, but a allOf()");
        }

        [Test]
        static void thatArgumentValueIsCapturedWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
        }

        [Test]
        static void thatCanPerformFurtherAssertionsOnCapturedArgumentValueWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            //When
            TestInnerClass testValue = new TestInnerClass();
            testValue.i = 4;
            testValue.s = "5";
            mockList.set(1, testValue);

            //Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(TestInnerClass));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).set(fflib_Match.anyInteger(),  argument.capture());
            object capturedArg = argument.getValue();
            System.assertNotEquals(null, capturedArg, "CapturedArg should not be null");
            System.assert(capturedArg is TestInnerClass, "CapturedArg should be SObject, instead was "+ capturedArg);
            TestInnerClass testValueCaptured = (TestInnerClass)capturedArg;
            System.assertEquals(4, testValueCaptured.i, "the values inside the argument captured should be the same of the original one");
            System.assertEquals("5", testValueCaptured.s, "the values inside the argument captured should be the same of the original one");
        }

        [Test]
        static void thatCaptureArgumentOnlyFromVerifiedMethodWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred");

            //the next call should be ignored because is not the method that has under verify,
            //even if have the same type specified in the capturer.
            mockList.addMore("Barney");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
            System.assertEquals(1, argument.getAllValues().size(), "the argument captured should be only one");
        }

        [Test]
        static void thatCaptureAllArgumentsForTheVerifiedMethodsWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});
            List<string> stringList = new List<string>{"3"};

            // When
            mockList.add("Fred");
            mockList.add(stringList);
            mockList.clear();

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)argument.capture());
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((List<string>)argument.capture());
            System.assertEquals(stringList, (List<string>)argument.getValue(), "the argument captured is not as expected");
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(2, argsCaptured.size(), "expected 2 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
        }

        [Test]
        static void thatCaptureArgumentFromRequestedParameterWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred", "Barney", "Wilma", "Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)fflib_Match.eq("Fred"),
				(string)fflib_Match.eq("Barney"),
				(string)argument.capture(),
				(string)fflib_Match.eq("Betty"));
            System.assertEquals("Wilma", (string)argument.getValue(),
			"the argument captured is not as expected, should be Wilma because is the 3rd parameter in the call");
        }

        [Test]
        static void thatCaptureLastArgumentWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Barney");
            mockList.add("Fred");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(2))).add((string)argument.capture());
            System.assertEquals("Fred", (string)argument.getValue(), "the argument captured is not as expected");
        }

        [Test]
        static void thatCaptureAllArgumentsWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred");
            mockList.add("Barney");
            mockList.add("Wilma");
            mockList.add("Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(4))).add((string)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(4, argsCaptured.size(), "expected 4 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[1], "the second value is not as expected");
            System.assertEquals("Wilma", (string)argsCaptured[2], "the third value is not as expected");
            System.assertEquals("Betty", (string)argsCaptured[3], "the forth value is not as expected");
        }

        [Test]
        static void thatCaptureAllArgumentsFromMultipleMethodsWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred");
            mockList.add("Barney");
            mockList.get2(3, "pebble");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(2))).add((string)argument.capture());
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).get2((int)fflib_Match.eq(3),
				(string)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(3, argsCaptured.size(), "expected 3 argument to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[1], "the second value is not as expected");
            System.assertEquals("pebble", (string)argsCaptured[2], "the third value is not as expected");
        }

        [Test]
        static void thatCanHandleMultipleCapturesInOneMethodCallWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Fred", "Barney", "Wilma", "Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)fflib_Match.eq("Fred"),
				(string)argument.capture(),
				(string)argument.capture(),
				(string)fflib_Match.eq("Betty"));
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(2, argsCaptured.size(), "expected 2 argument to be captured");
            System.assertEquals("Barney", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Wilma", (string)argsCaptured[1], "the second value is not as expected");
        }

        [Test]
        static void thatDoesNotCaptureIfNotVerifiedWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("3");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(List<string>));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.never())).add((List<string>)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(0, argsCaptured.size(), "expected 0 argument to be captured");
            System.assertEquals(null, argument.getValue(), "no value should be captured, so must return null");
        }

        [Test]
        static void thatCaptureOnlyMethodsThatMatchesWithOtherMatcherAsWellWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Same", "Same", "First call", "First call");
            mockList.add("Same", "Same", "Second call", "Second call");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add(fflib_Match.eqString("Same"),
			fflib_Match.eqString("Same"),
			(string)argument.capture(),
			fflib_Match.eqString("First call"));
            System.assertEquals("First call", (string)argument.getValue());
        }

        [Test]
        static void thatDoesNotCaptureAnythingWhenCaptorIsWrappedInAMatcherWithInOrderVerification()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_InOrder inOrder1 = new fflib_InOrder(mocks, new List<object>{mockList});

            // When
            mockList.add("Same", "Same", "First call", "First call");
            mockList.add("Same", "Same", "Second call", "Second call");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)inOrder1.verify(mockList, mocks.calls(1))).add((string)fflib_Match.allOf(fflib_Match.eqString("Same"),
				fflib_Match.eqString("Same"),
				argument.capture()),
			(string)fflib_Match.allOf(fflib_Match.eqString("Same"),
				fflib_Match.eqString("Same"),
				argument.capture()),
			(string)fflib_Match.allOf(argument.capture(),
				fflib_Match.eqString("First call")),
			(string)fflib_Match.allOf(argument.capture(),
				fflib_Match.eqString("First call")));
            List<object> capturedValues = argument.getAllValues();
            System.assertEquals(0, capturedValues.size(),
			"nothing should have been capture because the matcher it not really a capture type, but a allOf()");
            System.assertEquals(null, (string)argument.getValue(),
			"nothing should have been capture because the matcher it not really a capture type, but a allOf()");
        }

        [Test]
        static void thatCaptureAllArgumentswhenMethodIsCalledWithTheSameArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));

            // When
            mockList.add("Fred");
            mockList.add("Barney");
            mockList.add("Wilma");
            mockList.add("Barney");
            mockList.add("Barney");
            mockList.add("Betty");

            // Then
            fflib_ArgumentCaptor argument = fflib_ArgumentCaptor.forClass(typeof(string));
            ((fflib_MyList.IList)mocks.verify(mockList, 6)).add((string)argument.capture());
            List<object> argsCaptured = argument.getAllValues();
            System.assertEquals(6, argsCaptured.size(), "expected 6 arguments to be captured");
            System.assertEquals("Fred", (string)argsCaptured[0], "the first value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[1], "the second value is not as expected");
            System.assertEquals("Wilma", (string)argsCaptured[2], "the third value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[3], "the fourth value is not as expected");
            System.assertEquals("Barney", (string)argsCaptured[4], "the fifth value is not as expected");
            System.assertEquals("Betty", (string)argsCaptured[5], "the sixth value is not as expected");
        }

        private class TestInnerClass
        {
            public int i;

            public string s;
        }
    }
}
