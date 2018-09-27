package th.co.nssol.common.util;

import java.io.File;
import java.math.BigDecimal;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.w3c.dom.NodeList;

import th.co.nssol.services.model.data.WebserviceModel;


public class Constant {

	// @add by napatthita
	public static final String EMPTY = "";
	public static final String BLANK = "' '";
	public static final String HYPHEN = "-";
	

	public static final String TRUE = "1";
	public static final String FALSE = "0";

	public static final String LIST_TYPE = "List";
	

	public static String getConfigPathSystem() {
		File f = new File(CONF_PATH);
		if (f.exists() && f.isDirectory()) {
			return CONF_PATH + "\\";
		} else {
			return CONF_PATH_TEMP + "\\";
		}
	}

	// *** CONF_PATH ***
	public static final String WEB_ROOT = ClassPath.getInstance().getWebInfPath();
	
	public static final String IMG_PATH = WEB_ROOT + "pic\\";
	
	public static final String CONF_PATH = WEB_ROOT + "conf\\";
	public static final String CONF_PATH_TEMP = ".\\src\\main\\conf";
	public static final String CONF_PATH_SYSTEM = getConfigPathSystem();

	public static final String APPL_CONFIG_FILE_NAME = "ApplicationConfig.xml";
	public static final String WEBSERVICE_CONFIG_FILE_NAME = "webserviceConfig.xml";
	
	public static final String WEBSERVICE_MS_USER_CONFIG_FILE_NAME 	   = "webserviceTBLMSUSERConfig.xml";
	public static final String WEBSERVICE_MS_USER_ROLES_CONFIG_FILE_NAME = "webserviceTBLMSROLEUSERConfig.xml";
	public static final String WEBSERVICE_MS_MENU_ROLES_CONFIG_FILE_NAME = "webserviceTBLMSROLEMENUConfig.xml";
	public static final String WEBSERVICE_MS_MENU_CONFIG_FILE_NAME = "webserviceTBLMSMENUConfig.xml";
	
	public static final String WEBSERVICE_D365_PO_CONFIG_FILE_NAME = "webserviceTBLD365POConfig.xml";
	public static final String WEBSERVICE_D365_PDO_CONFIG_FILE_NAME = "webserviceTBLD365PDOConfig.xml";
	public static final String WEBSERVICE_D365_CPO_CONFIG_FILE_NAME = "webserviceTBLD365CPOConfig.xml";
	public static final String WEBSERVICE_D365_CUSTOMER_CONFIG_FILE_NAME = "webserviceTBLD365CUSTOMERConfig.xml";
	
	public static final String WEBSERVICE_PUR_RCV_CONFIG_FILE_NAME = "webserviceTBLRMPURRCVTRNConfig.xml";
	public static final String WEBSERVICE_SUPPLY_TRN_CONFIG_FILE_NAME = "webserviceTBLRMSUPPLYTRNConfig.xml";
	
	public static final String WEBSERVICE_TRN_PICKING_CONFIG_FILE_NAME = "webserviceTBLPICKINGConfig.xml";
	public static final String WEBSERVICE_TRN_SHIPPING_CONFIG_FILE_NAME = "webserviceTBLSHIPPINGConfig.xml";
	
	public static final String WEBSERVICE_TRN_OPER_LOG_CONFIG_FILE_NAME = "webserviceTBLOPERLOGConfig.xml";
	public static final String WEBSERVICE_TRN_INF_LOG_CONFIG_FILE_NAME = "webserviceTBLINFLOGConfig.xml";
	public static final String WEBSERVICE_TRN_LOGIN_LOG_CONFIG_FILE_NAME = "webserviceTBLLOGINLOGConfig.xml";
	
	
	public static final String XML_KEY_WEBSERVICE_FIELD_INSERT = "config:webservice_insert:item";
	public static final String XML_KEY_WEBSERVICE_TABLE_LIST = "config:webservice_insert:insert:table";
	public static final String XML_KEY_WEBSERVICE_GET_SQL = "config:webservice_get:sql";
	public static final String XML_KEY_WEBSERVICE_FIELD_GET = "config:webservice_get:item";
	public static final String XML_KEY_WEBSERVICE_FIELD_GET_RESULTCOLUMN = "config:webservice_get:result:column";
	

	public static final String XML_KEY_WEBSERVICE_LOGIN = "config:login";
	public static final String FIELD_GET = ":item";
	public static final String FIELD_GET_RESULTCOLUMN = ":result:column";
	public static final String SQL = ":sql";
	
	public static String getFileName(String fileName){
		return Constant.CONF_PATH_SYSTEM
				+ fileName;
	}
	
	public static List<WebserviceModel> GET_WEBSERVICE_MODEL(String fileName,String type){
		return ReadXML.getWebServiceValue(
				getFileName(fileName), type);
	}
	/*public static List<WebserviceModel> WEBSERVICE_INSERT(String fileName){
		return ReadXML.getWebServiceValue(
				getFileName(fileName), XML_KEY_WEBSERVICE_FIELD_INSERT);
	}
	public static List<WebserviceModel> WEBSERVICE_GET(String fileName){
		return ReadXML.getWebServiceValue(
				getFileName(fileName), XML_KEY_WEBSERVICE_FIELD_GET);
	}*/
	
	public static List<String> GET_STRING_LIST(String fileName,String tpye){
		return ReadXML.getStringListValue(
				getFileName(fileName), tpye);
	}
	/*public static List<String> WEBSERVICE_TABLELIST(String fileName){
		return ReadXML.getStringListValue(
				getFileName(fileName), XML_KEY_WEBSERVICE_TABLE_LIST);
	}
	public static List<String> WEBSERVICE_GET_RESULT(String fileName){
		return ReadXML.getStringListValue(
				getFileName(fileName), XML_KEY_WEBSERVICE_FIELD_GET_RESULT);
	}*/
	
	public static String WEBSERVICE_GET_SQL(String fileName,String type){
		return ReadXML.getValue(
				getFileName(fileName), type);
	}
	
	
	
	
	
	// invalid character
	public final static char RS = 0x1E;

	public static class functionId{

		public final static String FUNCID_LOGIN = "LOGIN";
		public final static String FUNCID_WEBSERVICE_USER = "MAS001";
		public final static String FUNCID_WEBSERVICE_USER_ROLE = "MAS002";
		public final static String FUNCID_WEBSERVICE_MENU_ROLE = "MAS003";
		public final static String FUNCID_WEBSERVICE_MENU = "MAS004";
		public final static String FUNCID_WEBSERVICE_PUR_RCV = "MAS005";
		public final static String FUNCID_WEBSERVICE_SUP_TRN = "MAS006";
		public final static String FUNCID_WEBSERVICE_PO = "MAS007";
		public final static String FUNCID_WEBSERVICE_PDO = "MAS008";
		public final static String FUNCID_WEBSERVICE_CPO = "MAS009";
		public final static String FUNCID_WEBSERVICE_CUSTOMER = "MAS010";
		public final static String FUNCID_WEBSERVICE_SHIPPING = "MAS011";

		public final static String FUNCID_WEBSERVICE_GET_SHIPPING = "FUND001";
		public final static String FUNCID_WEBSERVICE_GET_PICKED = "FUND002";
		public final static String FUNCID_WEBSERVICE_INSERT_SHIPPING = "FUND003";
		public final static String FUNCID_WEBSERVICE_INSERT_PICKED = "FUND004";
		

		public final static String FUNCNM_WEBSERVICE_LOGIN = "LOGIN";
		public final static String FUNCNM_WEBSERVICE_GET_SHIPPING = "GET_SHIPPING";
		public final static String FUNCNM_WEBSERVICE_GET_PICKED = "GET_PIKING";
		public final static String FUNCNM_WEBSERVICE_INSERT_SHIPPING = "INSERT_SHIPPING";
		public final static String FUNCNM_WEBSERVICE_INSERT_PICKED = "INSERT_PIKING";
		
		public final static Map<String,String> mappingFunction = mapData();
		
		private static Map<String,String> mapData(){
			Map<String,String> mappingFunction = new HashMap<String,String>();
			mappingFunction.put(FUNCID_WEBSERVICE_USER, WEBSERVICE_MS_USER_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_USER_ROLE, WEBSERVICE_MS_USER_ROLES_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_MENU_ROLE, WEBSERVICE_MS_MENU_ROLES_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_MENU, WEBSERVICE_MS_MENU_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_PUR_RCV, WEBSERVICE_PUR_RCV_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_SUP_TRN, WEBSERVICE_SUPPLY_TRN_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_PO, WEBSERVICE_D365_PO_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_PDO, WEBSERVICE_D365_PDO_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_CPO, WEBSERVICE_D365_CPO_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_CUSTOMER, WEBSERVICE_D365_CUSTOMER_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_SHIPPING, WEBSERVICE_TRN_SHIPPING_CONFIG_FILE_NAME);

			mappingFunction.put(FUNCID_WEBSERVICE_INSERT_PICKED, WEBSERVICE_TRN_PICKING_CONFIG_FILE_NAME);
			mappingFunction.put(FUNCID_WEBSERVICE_INSERT_SHIPPING, WEBSERVICE_TRN_SHIPPING_CONFIG_FILE_NAME);
			return mappingFunction;
		}
	}
	
	public class defaultField{

		public final static String DEL_FLG = "Delete_Flag";
		public final static String NEW_FLG = "New_Flag";
		public final static String CRE_DT = "Created_Date";
		//public final static String CRE_FUN_ID = "Cre_Fun_Id";
		public final static String CRE_USER_CD = "Created_User";
		public final static String LST_UPD_DT = "Updated_Date";
		//public final static String LST_UPD_FUN_ID = "Lst_Upd_Fun_Id";
		public final static String LST_UPD_USER_CD = "Updated_User";
		
	}
	public class logField{

		public final static String LOG_START = "START";
		public final static String LOG_END = "END";
		public final static String LOG_TYPE_INFO = "INFO";
		public final static String LOG_TYPE_ERROR = "ERROR";

		public final static String LOG_INSERT = ":Insert";
		public final static String LOG_GET = ":Get";
		
		
		public final static String FUNC_NAME = "FunctionName";
		public final static String LOG_TYPE = "LogType";
		public final static String CRE_DT = "Cre_Dt";
		public final static String LOG_DESC = "LogDesc";
		public final static String USER_ID = "UserId";
		
	}
	
}
