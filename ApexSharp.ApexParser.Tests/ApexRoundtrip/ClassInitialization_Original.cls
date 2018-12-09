public class ClassInitialization {

    public List<Contact> contactList;
    public ClassInitialization() {
        contactList = [SELECT ID FROM Contact LIMIT 1];
    }

    public static Map<String, String> colorMap = new Map<String, String>();

    static {
        colorMap.Put('red', '255, 0, 0');
        colorMap.Put('cyan', '0, 255, 255');
        colorMap.Put('magenta', '255, 0, 255');
    }
}