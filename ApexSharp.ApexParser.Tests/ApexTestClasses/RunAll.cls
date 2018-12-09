public class RunAll
{
    public RunAll()
    {
        //ClassAbstract classAbstract = new ClassAbstract();
        //ClassEnum classEnum = ClassEnum.America;
        ClassException classException = new ClassException();
        ClassGlobal classGlobal = new ClassGlobal();
        ClassInterface classInterface = new ClassInterface();

        ClassInternal classInternal = new ClassInternal();
        ClassInternal.InternalClassOne internalClassOne = new ClassInternal.InternalClassOne();
        ClassInternal.InternalClassTwo internalClassTwo = new ClassInternal.InternalClassTwo();

        ClassRest classRest = new ClassRest();

        ClassVirtual classVirtual = new ClassVirtual();

        ClassWithOutSharing classWithOutSharing = new ClassWithOutSharing();
        ClassWithSharing classWithSharing = new ClassWithSharing();


    }
}
