package pt.lighthouselabs.obd.reader.activity;

import java.text.SimpleDateFormat;
import java.util.Calendar;

/**
 * Created by Juris on 16.04.2015..
 */
public class DateHelperClass {

    public String GetDate(long milliSeconds)
    {
        // Create a DateFormatter object for displaying date in specified format.
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd kk:mm:ss.SS");

        // Create a calendar object that will convert the date and time value in milliseconds to date.
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(milliSeconds);
        return formatter.format(calendar.getTime());
    }
}
