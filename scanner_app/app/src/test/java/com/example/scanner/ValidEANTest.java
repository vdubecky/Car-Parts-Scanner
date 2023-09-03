package com.example.scanner;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import org.junit.Test;

public class ValidEANTest {

    @Test
    public void testMyFunction() {
        assertTrue(Utils.isValidEAN("1234567890128"));
        assertTrue(Utils.isValidEAN("1234563890122"));
        assertTrue(Utils.isValidEAN("1234563890122"));
        assertTrue(Utils.isValidEAN("9234563890124"));
        assertTrue(Utils.isValidEAN("9234563895129"));
        assertTrue(Utils.isValidEAN("4260646555210"));

        assertFalse(Utils.isValidEAN("1234567890120"));
        assertFalse(Utils.isValidEAN("1234567890122"));
        assertFalse(Utils.isValidEAN("123456789012"));
        assertFalse(Utils.isValidEAN("9234563890122"));
        assertFalse(Utils.isValidEAN("9234563895122"));
    }
}
