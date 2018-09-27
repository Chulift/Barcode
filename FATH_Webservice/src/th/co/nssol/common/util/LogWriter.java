package th.co.nssol.common.util;

import java.io.ByteArrayOutputStream;
import java.io.OutputStream;
import java.io.PrintStream;
import java.net.InetAddress;
import java.net.UnknownHostException;

import org.apache.log4j.Logger;

import th.co.nssol.common.framework.ApplicationException;


public class LogWriter {

	static Logger myLogger = null;

	static String address = null;

	public static void init(String packageName) {
		try {
			myLogger = Logger.getLogger(packageName);
			/* Get local computer name and address */
			InetAddress inet = InetAddress.getLocalHost();
			setAddress(inet.getHostName() + "[" + inet.getHostAddress() + "]");
		} catch (UnknownHostException e) {
			setAddress("UNKNOWN");
		}
	}

	private static void setAddress(String address) {
		LogWriter.address = address;
	}

	public static synchronized void info(String message) {
		init("INFO");
		myLogger.info(LogWriter.address + " : " + message);
	}

	public static synchronized void error(String message) {
		init("ERROR");
		myLogger.error(LogWriter.address + " : " + message);
	}

	public static void error(Exception e) {
		ApplicationException ae = null;
		if (!(e instanceof ApplicationException)) {
			ae = new ApplicationException(e);
		} else {
			ae = (ApplicationException) e;
		}
		LogWriter.error(ae.getMsg() == null ? "" : ae.getMsg(), ae
				.getException());
	}

	public static synchronized void error(String message, Throwable t) {
		init("ERROR");
		if (t != null) {
			OutputStream buf = new ByteArrayOutputStream();
			PrintStream p = new PrintStream(buf);
			t.printStackTrace(p);
			myLogger.error(LogWriter.address + " : " + message + " "
					+ buf.toString());

		} else {
			myLogger.error(LogWriter.address + " : " + message);
		}
	}
}