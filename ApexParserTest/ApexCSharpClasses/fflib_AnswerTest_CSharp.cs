namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using NUnit.Framework;

    /*
     Copyright (c) 2017 FinancialForce.com, inc.  All rights reserved.
     */
    /**
     * @nodoc
     */
    [TestFixture]
    private class fflib_AnswerTest
    {
        private static fflib_InvocationOnMock actualInvocation = null;

        [Test]
        static void thatAnswersWithException()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.ExceptionForAnswer());
            mocks.stopStubbing();

            // When
            try
            {
                mockList.get2(0, "Hi hi Hello Hi hi");
                System.assert(false, "an exception is expected to be thrown on the answer execution");
            }
            catch (fflib_ApexMocks.ApexMocksException ansExpt)
            {
                string expectedMessage = "an error occurs on the execution of the answer";

                // Then
                System.assertEquals(expectedMessage, ansExpt.getMessage(), "the message from the answer is not as expected");
            }
        }

        [Test]
        static void thatStoresMethodIntoInvocationOnMock()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.BasicAnswer());
            mocks.stopStubbing();

            // When
            mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            object methodCalled = actualInvocation.getMethod();
            System.assert(methodCalled instanceof fflib_QualifiedMethod, "the object returned is not a method as expected");
            string expectedMethodSignature = fflib_MyList.getStubClassName()+ ".get2(Integer, String)";
            System.assertEquals(expectedMethodSignature, ((fflib_QualifiedMethod)methodCalled).toString(), " the method is no the one expected");
        }

        [Test]
        static void thatAnswerOnlyForTheMethodStubbedWithAnswer()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(3)).thenReturn("ted");
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.BasicAnswer());
            mocks.stopStubbing();

            // When
            mockList.add("one");
            string noAnswered = mockList.get(3);
            mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            object methodCalled = actualInvocation.getMethod();
            System.assert(methodCalled instanceof fflib_QualifiedMethod, "the object returned is not a method as expected");
            string expectedMethodSignature = fflib_MyList.getStubClassName()+ ".get2(Integer, String)";
            System.assertEquals(expectedMethodSignature, ((fflib_QualifiedMethod)methodCalled).toString(), " the method is no the one expected");
            System.assertEquals("ted", noAnswered, "the get method should have returned the stubbed string");
        }

        [Test]
        static void thatMultipleAnswersAreHandled()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get(3)).thenAnswer(new fflib_AnswerTest.FirstAnswer());
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.SecondAnswer());
            mocks.stopStubbing();

            // When
            mockList.add("one");
            string answer1 = mockList.get(3);
            string answer2 = mockList.get2(0, "Hi hi Hello Hi hi");
            System.assertEquals("this is the first answer", answer1, "the answer wasnt the one expected");
            System.assertEquals("and this is the second one", answer2, "the answer wasnt the one expected");
        }

        [Test]
        static void thatStoresMockInstanceIntoInvocationOnMock()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.BasicAnswer());
            mocks.stopStubbing();

            // When
            string mockCalled = mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            System.assert(actualInvocation.getMock()instanceof fflib_MyList.IList, "the object returned is not a mock instance as expected");
            System.assertEquals(mockList, actualInvocation.getMock(), "the mock returned should be the mockList used in the stubbing");
        }

        [Test]
        static void thatMethodsParametersAreAccessible()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.ProcessArgumentAnswer());
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            System.assertEquals("Bye hi Hello Bye hi", actualValue, "the answer is not correct");
        }

        [Test]
        static void thatAnswerOnlyForTheStubbedParameter()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.ProcessArgumentAnswer());
            mocks.stopStubbing();

            // When
            string actualValue1 = mockList.get2(0, "some string for my method");
            string actualValue2 = mockList.get2(0, "Hi hi Hello Hi hi");
            string actualValue3 = mockList.get2(0, "another string for the same method");

            // Then
            System.assertEquals("Bye hi Hello Bye hi", actualValue2, "the answer is not correct");
            System.assertEquals(null, actualValue1, "the answer is not correct");
            System.assertEquals(null, actualValue3, "the answer is not correct");
        }

        [Test]
        static void thatMethodsParametersAreAccessibleWhenCalledWithMatchers()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(fflib_Match.anyInteger(), fflib_Match.anyString())).thenAnswer(new fflib_AnswerTest.ProcessArgumentAnswer());
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get2(0, "Hi hi Hello Hi hi");

            // Then
            System.assertEquals("Bye hi Hello Bye hi", actualValue, "the answer is not correct");
        }

        [Test]
        static void thatExceptionIsThrownWhenAccessOutOfIndexArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.ExceptionForArgumentsOutOfBound());
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get2(0, "Hi hi Hello Hi hi");
        }

        [Test]
        static void thatExceptionIsThrownWhenAccessNegativeIndexArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.get2(0, "Hi hi Hello Hi hi")).thenAnswer(new fflib_AnswerTest.ExceptionForNegativeArgumentIndex());
            mocks.stopStubbing();

            // When
            string actualValue = mockList.get2(0, "Hi hi Hello Hi hi");
        }

        [Test]
        static void thatArgumentListEmptyForMethodWithNoArgument()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            mocks.when(mockList.isEmpty()).thenAnswer(new fflib_AnswerTest.ArgumentListEmptyForMethodWithNoArgument());
            mocks.stopStubbing();

            // When
            bool actualValue = mockList.isEmpty();
        }

        [Test]
        static void thatAnswerToVoidMethod()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList)mocks.doAnswer(new fflib_AnswerTest.BasicAnswer(), mockList)).addMore("Hi hi Hello Hi hi");
            mocks.stopStubbing();

            // When
            mockList.addMore("Hi hi Hello Hi hi");

            // Then
            object methodCalled = actualInvocation.getMethod();
            System.assert(methodCalled instanceof fflib_QualifiedMethod, "the object returned is not a method as expected");
            string expectedMethodSignature = fflib_MyList.getStubClassName()+ ".addMore(String)";
            System.assertEquals(expectedMethodSignature, ((fflib_QualifiedMethod)methodCalled).toString(), "Unexpected method name: "+ methodCalled);
        }

        [Test]
        static void thatAnswerToVoidAndNotVoidMethods()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            mocks.startStubbing();
            ((fflib_MyList)mocks.doAnswer(new fflib_AnswerTest.FirstAnswer(), mockList)).get(3);
            ((fflib_MyList)mocks.doAnswer(new fflib_AnswerTest.BasicAnswer(), mockList)).addMore("Hi hi Hello Hi hi");
            ((fflib_MyList)mocks.doAnswer(new fflib_AnswerTest.SecondAnswer(), mockList)).get2(4, "Hi hi Hello Hi hi");
            mocks.stopStubbing();

            // When
            string answer1 = mockList.get(3);
            string answer2 = mockList.get2(4, "Hi hi Hello Hi hi");
            mockList.addMore("Hi hi Hello Hi hi");

            // Then
            object methodCalled = actualInvocation.getMethod();
            System.assert(methodCalled instanceof fflib_QualifiedMethod, "the object returned is not a method as expected");
            string expectedMethodSignature = fflib_MyList.getStubClassName()+ ".addMore(String)";
            System.assertEquals(expectedMethodSignature, ((fflib_QualifiedMethod)methodCalled).toString(),
			"the last method called should be the addMore, so should be the last to set the actualInvocation variable.");
            System.assertEquals("this is the first answer", answer1, "the answer was not the one expected");
            System.assertEquals("and this is the second one", answer2, "the answer was not the one expected");
        }

        [Test]
        static void thatAnswerToDifferentVoidMethods()
        {
            // Given
            fflib_ApexMocks mocks = new fflib_ApexMocks();
            fflib_MyList mockList = (fflib_MyList)mocks.mock(typeof(fflib_MyList));
            fflib_AnswerTest.FirstAnswer answer1 = new fflib_AnswerTest.FirstAnswer();
            fflib_AnswerTest.SecondAnswer answer2 = new fflib_AnswerTest.SecondAnswer();
            System.assertEquals(null, answer1.getMessage(), "the answer message should be null at this stage");
            System.assertEquals(null, answer2.getMessage(), "the answer message should be null at this stage");
            mocks.startStubbing();
            ((fflib_MyList)mocks.doAnswer(answer1, mockList)).addMore("Hi hi Hello Hi hi");
            ((fflib_MyList)mocks.doAnswer(answer2, mockList)).add("Hello");
            mocks.stopStubbing();

            // When
            mockList.addMore("Hi hi Hello Hi hi");
            mockList.add("Hello");

            // Then
            System.assertEquals("this is the first answer", answer1.getMessage(), "the answer was not the one expected");
            System.assertEquals("and this is the second one", answer2.getMessage(), "the answer was not the one expected");
        }

        //Answers
        public class BasicAnswer : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                return null;
            }
        }

        public class ExceptionForAnswer : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                throw new fflib_ApexMocks.ApexMocksException("an error occurs on the execution of the answer");
                return null;
            }
        }

        public class ExceptionForArgumentsOutOfBound : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                try
                {
                    object noExistingObj = invocation.getArgument(2);
                    System.assert(false, "an exception was expected because the argument in the method are only 2");
                }
                catch (fflib_ApexMocks.ApexMocksException exp)
                {
                    string expectedMessage = "Invalid index, must be greater or equal to zero and less of 2.";
                    string actualMessage = exp.getMessage();
                    System.assertEquals(expectedMessage, actualMessage, "the message return by the exception is not as expected");
                }

                return null;
            }
        }

        public class ExceptionForNegativeArgumentIndex : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                try
                {
                    object noExistingObj = invocation.getArgument(-1);
                    System.assert(false, "an exception was expected because the argument index cannot be negative");
                }
                catch (fflib_ApexMocks.ApexMocksException exp)
                {
                    string expectedMessage = "Invalid index, must be greater or equal to zero and less of 2.";
                    string actualMessage = exp.getMessage();
                    System.assertEquals(expectedMessage, actualMessage, "the message return by the exception is not as expected");
                }

                return null;
            }
        }

        public class ArgumentListEmptyForMethodWithNoArgument : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                List<object> emptyList = invocation.getArguments();
                System.assertEquals(0, emptyList.size(), "the argument list from a method without arguments should be empty");
                return null;
            }
        }

        public class FirstAnswer : fflib_Answer
        {
            private string answerMessage;

            public string getMessage()
            {
                return this.answerMessage;
            }

            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                this.answerMessage = "this is the first answer";
                return answerMessage;
            }
        }

        public class SecondAnswer : fflib_Answer
        {
            private string answerMessage;

            public string getMessage()
            {
                return this.answerMessage;
            }

            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                this.answerMessage = "and this is the second one";
                return answerMessage;
            }
        }

        public class ProcessArgumentAnswer : fflib_Answer
        {
            public object answer(fflib_InvocationOnMock invocation)
            {
                actualInvocation = invocation;
                string argument = (string)invocation.getArgument(1);
                System.assertNotEquals(null, argument, " the argument should have some value");
                argument = argument.replace("Hi", "Bye");
                return argument;
            }
        }
    }
}
