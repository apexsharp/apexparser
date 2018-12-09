public class GetSetDemo {
    public double MyReadWritePropPublic { get; set; }
    private Integer propPrvt;
    public Integer prop {
        get {
            return propPrvt;
        }
        set {
            propPrvt = value;
        }
    }
    public Integer MyReadOnlyProp {
        get;
    }

    private static Integer myStaticPropPrvt;
    public static Integer MyStaticProp {
        get {
            return myStaticPropPrvt;
        }
    }

    Integer MyReadOnlyPropPrvt { get;}
    double MyReadWritePropPrvt {get; set;
    }
    public string MyWriteOnlyProp {get; }
    protected string MyWriteOnlyPropPrvt { set; get;
    }

    private string namePrvt;

    public string getName {
        get {
            return namePrvt;
        }
        private set {
            namePrvt = value;
        }
    }
    public string Stubbing {
        private get {
            return namePrvt;
        }
        set {
            namePrvt = value;
        }
    }
    public string DoThrowWhenException {
        get {
            return namePrvt;
        }
        set {
            namePrvt = value;
        }
    }
}