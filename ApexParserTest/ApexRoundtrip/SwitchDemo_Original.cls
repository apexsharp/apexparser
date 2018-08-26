public class SwitchDemo {
    public static void Method() {
        Integer x = 123;
        switch on x {
        	when 5, 6, 7 {
        		System.debug('Cool!'); // 1
        	}
        	when String c {
        		switch on c {
        			// 2
        			when 'foo' {
        				System.debug('bar'); // 3
        			}
        			when else {
        				System.debug('baz'); // 4
        			} // 5
        		}

        		System.debug('corge');
        	} // 6
        	when else {
        		return; // 7
        	}
        }
        while (true) {
        	System.debug('quux'); // 8
        }
    }
}