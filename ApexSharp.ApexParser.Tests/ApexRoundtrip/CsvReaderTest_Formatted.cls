@IsTest
class CsvReaderTest
{
    @IsTest
    static void testCSVReader1()
    {
        String csvString = 'fieldName1,fieldName2,fieldName3,fieldName4\r\n'+
                '"valu,e a1","value\nb1",value c1,\n'+
                'value a2,"value""b2","valu""e c2",\r\n'+
                ',value\"b3,value\'c3,\'value d3\'\n'+
                '"value,a4","",,\'value d4\'';
        List<List<String>> data = CsvReader.read(csvString);
        System.assertEquals('valu,e a1', data[1][0]);
        System.assertEquals('value\nb1', data[1][1]);
        System.assertEquals('value c1', data[1][2]);
        System.assertEquals('', data[1][3]);
        System.assertEquals('value a2', data[2][0]);
        System.assertEquals('value"b2', data[2][1]);
        System.assertEquals('valu"e c2', data[2][2], 'Expected handling of double quoted quotes');
        System.assertEquals('', data[2][3]);
        System.assertEquals('', data[3][0]);
        System.assertEquals('value"b3', data[3][1]);
        System.assertEquals('value\'c3', data[3][2]);
        System.assertEquals('\'value d3\'', data[3][3]);
        System.assertEquals('value,a4', data[4][0]);
        System.assertEquals('', data[4][1]);
        System.assertEquals('', data[4][2]);
        System.assertEquals('\'value d4\'', data[4][3]);
    }

    @IsTest
    static void testCSVReader2()
    {
        String csvString = 'fieldName1\r\n'+
                '"valu,e a1"\n'+
                'value a2\n'+
                '\n'+
                '""\n'+
                '"value,a4"';
        List<List<String>> data = CsvReader.read(csvString);
        System.assertEquals('valu,e a1', data[1][0]);
        System.assertEquals('value a2', data[2][0]);
        System.assertEquals('', data[3][0]);
        System.assertEquals('', data[4][0]);
        System.assertEquals('value,a4', data[5][0]);
    }
}
