namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;
    using Apex.NUnit;

    /**
     * Copyright (c) 2014, FinancialForce.com, inc
     * All rights reserved.
     *
     * Redistribution and use in source and binary forms, with or without modification,
     * are permitted provided that the following conditions are met:
     *
     * - Redistributions of source code must retain the above copyright notice,
     *      this list of conditions and the following disclaimer.
     * - Redistributions in binary form must reproduce the above copyright notice,
     *      this list of conditions and the following disclaimer in the documentation
     *      and/or other materials provided with the distribution.
     * - Neither the name of the FinancialForce.com, inc nor the names of its contributors
     *      may be used to endorse or promote products derived from this software without
     *      specific prior written permission.
     *
     * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
     * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
     * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
     * THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
     * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
     * OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
     * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
     * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
     */
    [TestFixture]
    private class fflib_IDGeneratorTest
    {
        [Test]
        static void itShouldGenerateValidIDs()
        {
            string id1 = fflib_IDGenerator.generate(Account.SObjectType);
            string id2 = fflib_IDGenerator.generate(Account.SObjectType);
            string id3 = fflib_IDGenerator.generate(Account.SObjectType);
            string id4 = fflib_IDGenerator.generate(Account.SObjectType);
            string id5 = fflib_IDGenerator.generate(Account.SObjectType);
            string id6 = fflib_IDGenerator.generate(Account.SObjectType);
            string id7 = fflib_IDGenerator.generate(Account.SObjectType);
            string id8 = fflib_IDGenerator.generate(Account.SObjectType);
            string id9 = fflib_IDGenerator.generate(Account.SObjectType);
            string id10 = fflib_IDGenerator.generate(Account.SObjectType);
            string id11 = fflib_IDGenerator.generate(Account.SObjectType);
            System.assertEquals("001000000000001AAA", id1);
            System.assertEquals("001000000000002AAA", id2);
            System.assertEquals("001000000000003AAA", id3);
            System.assertEquals("001000000000004AAA", id4);
            System.assertEquals("001000000000005AAA", id5);
            System.assertEquals("001000000000006AAA", id6);
            System.assertEquals("001000000000007AAA", id7);
            System.assertEquals("001000000000008AAA", id8);
            System.assertEquals("001000000000009AAA", id9);
            System.assertEquals("001000000000010AAA", id10);
            System.assertEquals("001000000000011AAA", id11);
        }
    }
}
