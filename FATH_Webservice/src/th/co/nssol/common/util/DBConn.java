/*
 * ConnectDB.java
 *
 * Created on February 27, 2007, 3:38 PM
 *
 * To change this template, choose Tools | Options and locate the template under
 * the Source Creation and Management node. Right-click the template and choose
 * Open. You can then make changes to the template in the Source Editor.
 */

package th.co.nssol.common.util;

import java.sql.*;

public final class DBConn {

	private static final String XML_CONF_FILE_NAME = Constant.CONF_PATH
			+ Constant.APPL_CONFIG_FILE_NAME;

	private static final String XML_KEY_URL = "config:database:url";

	private static final String XML_KEY_DRIVER = "config:database:driver-name";

	private static final String XML_KEY_USERNAME = "config:database:username";

	private static final String XML_KEY_PASSWORD = "config:database:password";

	private static final String DATABASE_URL = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_URL);

	private static final String DATABASE_DRIVER = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_DRIVER);
	
	private static String USERNAME = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_USERNAME);

	private static String PASSWORD = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_PASSWORD);
	
	private DBConn() {
	}

	public static Connection getConnection() {
		String DbUrl = "";
		Connection conn = null;
		try {
			
			Class.forName(DATABASE_DRIVER);

			DbUrl = DATABASE_URL;
			
			conn = DriverManager.getConnection(DATABASE_URL, USERNAME, PASSWORD);
			conn.setTransactionIsolation(Connection.TRANSACTION_READ_COMMITTED);
			conn.setAutoCommit(false);
		} catch (Exception e) {
			LogWriter.error("getConnection() error. URL=" + DbUrl + ".",
					e);
		}
		return conn;
	}

	public static Connection getConnection(String driver, String url, String username, String password) {
		Connection conn = null;
		try {
			Class.forName(driver);
			conn = DriverManager.getConnection(url,username,password);
			conn.setAutoCommit(false);
		} catch (Exception e) {
			LogWriter.error("getConnection() error. URL=" + url + ".", e);
		}
		return conn;
	}

	public static void closeConnection(Connection conn) {
		if (conn != null) {
			try {
				conn.rollback();
			} catch (Exception e) {
				LogWriter.error("closeConnection() rollback error.", e);
			} finally {
				try {
					conn.close();
				} catch (Exception e) {
					LogWriter.error("closeConnection() connection error.", e);
				}
			}
		}
	}
}
