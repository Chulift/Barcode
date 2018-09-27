package th.co.nssol.services.model.logic;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.security.MessageDigest;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;

import javax.jws.WebParam;
import javax.jws.WebService;
import javax.xml.bind.DatatypeConverter;
import javax.xml.soap.MessageFactory;
import javax.xml.soap.MimeHeaders;
import javax.xml.soap.SOAPBody;
import javax.xml.soap.SOAPEnvelope;
import javax.xml.soap.SOAPMessage;
import javax.xml.soap.SOAPPart;

import net.arnx.jsonic.JSON;

import org.apache.log4j.Logger;
import org.apache.log4j.PatternLayout;
import org.apache.log4j.RollingFileAppender;
import org.seasar.util.beans.util.BeanMap;
import org.seasar.util.lang.StringUtil;

import th.co.nssol.common.util.Constant;
import th.co.nssol.common.util.DBConn;
import th.co.nssol.common.util.LogWriter;
import th.co.nssol.services.model.data.WebserviceModel;

import com.elminster.easydao.db.manager.DAOSupportManager;
import com.fasterxml.jackson.databind.ObjectMapper;

import antlr.StringUtils;

@WebService(targetNamespace = "http://logic.model.services.nssol.co.th/", portName = "ServicesBLPort", serviceName = "ServicesBLService")
public class ServicesBL{
	
	public ServicesBL(){
		
	}

	public String getConnectD365(@WebParam(name = "userId") String userId){
		
		String result = "";
		try{
			WebserviceConnect webserviceConnect = new WebserviceConnect();
			result = userId + " : " +webserviceConnect.getD365();
		}catch(Exception e){
			result = e.toString();
			writeLog(""
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,result);
		}
		return result;
	}
	
	public String getConnectAX(@WebParam(name = "userId") String userId){
		
		String result = "";
		try{
			WebserviceConnectAX webserviceConnect = new WebserviceConnectAX();
			result = userId + " : " +webserviceConnect.getAX();
		}catch(Exception e){
			result = e.toString();
			writeLog(""
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,result);
		}
		return result;
	}
	
	public String getMasterData(
			@WebParam(name = "function") String function,
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		writeLog(function,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START + Constant.logField.LOG_GET);
		
		String fileName = Constant.functionId.mappingFunction.get(function);
		if(!StringUtil.isEmpty(fileName)) {
			
			// Search result
			try{
				ObjectMapper mapper = new ObjectMapper();
				Map<String,Object> map = mapper.readValue(object, Map.class);
				
				result = getData(fileName,map,userId,function);
				
			}catch(Exception e){
				writeLog(function
						,Constant.logField.LOG_TYPE_ERROR
						,userId
						,e.toString());
				return e.toString();
			}
		}
		
		writeLog(function,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_END + Constant.logField.LOG_GET);
		return result;
	}
	
	public String insertMasterData(
			@WebParam(name = "function") String function,
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		writeLog(function
				,Constant.logField.LOG_TYPE_INFO
				,userId
				,Constant.logField.LOG_START + Constant.logField.LOG_INSERT);
		String fileName = Constant.functionId.mappingFunction.get(function);
		// insert result
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			
			result = insertData(fileName,map,userId,function);
			
		}catch(Exception e){
			writeLog(function
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,e.toString());

			return e.toString();
		}
		writeLog(function
				,Constant.logField.LOG_TYPE_INFO
				,userId
				,Constant.logField.LOG_END + Constant.logField.LOG_INSERT);
		return result;
	}
	
	private String insertData(String fileName,Map<String,Object> object,String userId,String function) {
		boolean result = true;
		String returnResult = "";
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
		// Search result
		try{
			conn = DBConn.getConnection();
			if(conn != null){
				//current date
				java.sql.Timestamp currentDate = new java.sql.Timestamp((new java.util.Date()).getTime());
				// user id
				String currentLoginCd = userId;
						
				List<WebserviceModel> pattern = Constant.GET_WEBSERVICE_MODEL(fileName,Constant.XML_KEY_WEBSERVICE_FIELD_INSERT);
				for(int i = 0 ; i < pattern.size() ; i++){
					pattern.get(i).data =  object.get(pattern.get(i).key).toString();
					//System.err.println("data - " + pattern.get(i).key + " : "+ object.get(pattern.get(i).key));
				}
				
				List<String> tabelList = Constant.GET_STRING_LIST(fileName,Constant.XML_KEY_WEBSERVICE_TABLE_LIST);
				//List<String> params = new ArrayList<String>();
				//List<String> params2 = new ArrayList<String>();
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				
				for(String tableName : tabelList){
					sql = new StringBuilder();
					sql2 = new StringBuilder();
					sql.append("INSERT INTO "+ tableName+" (");
					sql2.append("VALUES(");
					
					int i = 0;
					boolean endFlg = false;
					for(WebserviceModel detail: pattern){
						if(tableName.equalsIgnoreCase(detail.table)){
							if(!"1".equals(detail.endFlag)) {
								sql.append(" "+ detail.field +" ,");
								sql2.append(" '" + detail.data + "' ,");
							}else{
								sql.append(" "+ detail.field +" ");
								sql2.append(" '" + detail.data + "' ");
								endFlg = true;
							}
						}
					}
					if(!endFlg) {
						//START INPUT DEFULT FIELD 
						sql.append(" "+ Constant.defaultField.DEL_FLG +" ,");
						sql2.append(" '" + Constant.FALSE + "' ,");
			
						sql.append(" "+ Constant.defaultField.NEW_FLG +" ,");
						sql2.append(" '" + Constant.TRUE + "' ,");
			
						sql.append(" "+ Constant.defaultField.CRE_DT +" ,");
						sql2.append(" '" + currentDate + "' ,");
			
						//sql.append(" "+ Constant.defaultField.CRE_FUN_ID +" ,");
						//sql2.append(" '" + Constant.FUNCID_WEBSERVICE_INSERT + "' ,");
			
						sql.append(" "+ Constant.defaultField.CRE_USER_CD +" ,");
						sql2.append(" '" + currentLoginCd + "' ,");
			
						sql.append(" "+ Constant.defaultField.LST_UPD_DT +" ,");
						sql2.append(" '" + currentDate + "' ,");
			
						//sql.append(" "+ Constant.defaultField.LST_UPD_FUN_ID +" ,");
						//sql2.append(" '" + Constant.FUNCID_WEBSERVICE_INSERT  + "' ,");
			
						sql.append(" "+ Constant.defaultField.LST_UPD_USER_CD +" ");
						sql2.append(" '" + currentLoginCd + "' ");
						//END INPUT DEFULT FIELD 
					}
					
					sql.append(")");
					sql2.append(")");
					
					sql.append(sql2.toString());
					
					stmt = conn.createStatement();  
			        int status = stmt.executeUpdate(sql.toString());
			        if(status != 1){
			        	result = false;
			        }
				}
				if(result){
					conn.commit();
					returnResult = "success";
				}
				
			}
		}catch (Exception e){
			LogWriter.error(e);
			returnResult = e.toString();
			writeLog(function
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}	
		return returnResult;
	}
	
	private String insertListData(String fileName,List<Map<String,Object>> listObject,String userId,String function) {
		boolean result = true;
		String returnResult = "";
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
		// Search result
		try{
			conn = DBConn.getConnection();
			if(conn != null){
				//current date
				java.sql.Timestamp currentDate = new java.sql.Timestamp((new java.util.Date()).getTime());
				// user id
				String currentLoginCd = userId;
						
				List<WebserviceModel> pattern = Constant.GET_WEBSERVICE_MODEL(fileName,Constant.XML_KEY_WEBSERVICE_FIELD_INSERT);
				List<String> tabelList = Constant.GET_STRING_LIST(fileName,Constant.XML_KEY_WEBSERVICE_TABLE_LIST);
				
				
				//List<String> params = new ArrayList<String>();
				//List<String> params2 = new ArrayList<String>();
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				for(Map<String,Object> object : listObject) {
					
					for(int i = 0 ; i < pattern.size() ; i++){
						pattern.get(i).data =  object.get(pattern.get(i).key).toString();
						//System.err.println("data - " + pattern.get(i).key + " : "+ object.get(pattern.get(i).key));
					}
					for(String tableName : tabelList){
						sql = new StringBuilder();
						sql2 = new StringBuilder();
						sql.append("INSERT INTO "+ tableName+" (");
						sql2.append("VALUES(");
						
						int i = 0;
						boolean endFlg = false;
						for(WebserviceModel detail: pattern){
							if(tableName.equalsIgnoreCase(detail.table)){
								if(!"1".equals(detail.endFlag)) {
									sql.append(" "+ detail.field +" ,");
									sql2.append(" '" + detail.data + "' ,");
								}else{
									sql.append(" "+ detail.field +" ");
									sql2.append(" '" + detail.data + "' ");
									endFlg = true;
								}
								
							}
						}
						if(!endFlg) {
							//START INPUT DEFULT FIELD 
							sql.append(" "+ Constant.defaultField.DEL_FLG +" ,");
							sql2.append(" '" + Constant.FALSE + "' ,");
				
							sql.append(" "+ Constant.defaultField.NEW_FLG +" ,");
							sql2.append(" '" + Constant.TRUE + "' ,");
				
							sql.append(" "+ Constant.defaultField.CRE_DT +" ,");
							sql2.append(" '" + currentDate + "' ,");
				
							//sql.append(" "+ Constant.defaultField.CRE_FUN_ID +" ,");
							//sql2.append(" '" + Constant.FUNCID_WEBSERVICE_INSERT + "' ,");
				
							sql.append(" "+ Constant.defaultField.CRE_USER_CD +" ,");
							sql2.append(" '" + currentLoginCd + "' ,");
				
							sql.append(" "+ Constant.defaultField.LST_UPD_DT +" ,");
							sql2.append(" '" + currentDate + "' ,");
				
							//sql.append(" "+ Constant.defaultField.LST_UPD_FUN_ID +" ,");
							//sql2.append(" '" + Constant.FUNCID_WEBSERVICE_INSERT  + "' ,");
				
							sql.append(" "+ Constant.defaultField.LST_UPD_USER_CD +" ");
							sql2.append(" '" + currentLoginCd + "' ");
							//END INPUT DEFULT FIELD 
						}
						
						sql.append(")");
						sql2.append(")");
						
						sql.append(sql2.toString());
						
						stmt = conn.createStatement();  
				        int status = stmt.executeUpdate(sql.toString());
				        if(status != 1){
				        	result = false;
				        }
					}
				}
				
				if(result){
					conn.commit();
					returnResult = "success";
					System.err.println("Insert Transaction : " + returnResult);
				}else {
					conn.rollback();
					returnResult = "fail";
					System.err.println("Insert Transaction : " + returnResult);
				}
				
			}
		}catch (Exception e){
			LogWriter.error(e);
			returnResult = e.toString();
			writeLog(function
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}	
		return returnResult;
	}
	
	private String getData(String fileName,Map<String,Object> object,String userId,String function){
		String result = "";
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
		
		try{
			conn = DBConn.getConnection();
			if(conn != null){
				List<WebserviceModel> pattern = Constant.GET_WEBSERVICE_MODEL(fileName,Constant.XML_KEY_WEBSERVICE_FIELD_GET);
				for(int i = 0 ; i < pattern.size() ; i++){
					pattern.get(i).data =  object.get(pattern.get(i).key).toString();
					//System.err.println("data - " + pattern.get(i).key + " : "+ object.get(pattern.get(i).key));
				}
				
				String sqlXml = Constant.WEBSERVICE_GET_SQL(fileName,Constant.XML_KEY_WEBSERVICE_GET_SQL);
				List<String> resultSet = Constant.GET_STRING_LIST(fileName,Constant.XML_KEY_WEBSERVICE_FIELD_GET_RESULTCOLUMN);
				//List<String> params = new ArrayList<String>();
				//List<String> params2 = new ArrayList<String>();
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				sql.append(sqlXml);
				for(WebserviceModel detail: pattern){
					String data = object.get(detail.key).toString();
					sql.append(" AND "+ detail.table + "." +  detail.field + " = '" + data +"' ");
				}
				stmt = conn.createStatement();  
		        rs = stmt.executeQuery(sql.toString());
		        BeanMap resultData = new BeanMap();
		        while(rs.next()) {
		        	for(String key : resultSet){
		        		resultData.put(key, rs.getString(key));
		        	}
		        } 
				result = JSON.encode(resultData);
				//result = "success";
			}else{
				result = "fail";
			}
			
		}catch (Exception e){
			LogWriter.error(e);
			result = e.toString();
			writeLog(function
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}
		return result;
	}
	
	private String setString(String content){
		if(!StringUtil.isEmpty(content)){
			if(content.contains("'")){
				content = content.replaceAll("'", "''");
			}
		}
		
		return content;
	}
	
	private void writeLog(String functionName,String logType,String userId,String logDesc){
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
		// Search result
		try{
			conn = DBConn.getConnection();
			if(conn != null){
				//current date
				java.sql.Timestamp currentDate = new java.sql.Timestamp((new java.util.Date()).getTime());
				
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				
				sql = new StringBuilder();
				sql2 = new StringBuilder();
				sql.append("INSERT INTO "+ "TBL_ERR_LOG" +" (");
				sql2.append("VALUES(");
				
				//START INPUT DEFULT FIELD 
				sql.append(" "+ Constant.logField.FUNC_NAME +" ,");
				sql2.append(" '" + functionName + "' ,");
	
				sql.append(" "+ Constant.logField.LOG_TYPE +" ,");
				sql2.append(" '" + logType + "' ,");
	
				sql.append(" "+ Constant.logField.LOG_DESC +" ,");
				sql2.append(" '" + logDesc + "' ,");
	
				sql.append(" "+ Constant.logField.USER_ID +" ,");
				sql2.append(" '" + userId + "' ,");
	
				sql.append(" "+ Constant.logField.CRE_DT +" ");
				sql2.append(" '" + currentDate + "' ");
				//END INPUT DEFULT FIELD 
				
				sql.append(")");
				sql2.append(")");
				
				sql.append(sql2.toString());
				
				stmt = conn.createStatement();  
		        stmt.executeUpdate(sql.toString());
		        conn.commit();
			}
		}catch (Exception e){
			LogWriter.error(e);
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}	
	}
	
	public String getLogin(
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
	    String fileName = Constant.WEBSERVICE_CONFIG_FILE_NAME;
	    /*writeLog(Constant.functionId.FUNCID_LOGIN,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START);*/
	    insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_LOGIN
				,Constant.functionId.FUNCID_LOGIN,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_START);
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			MessageDigest md = MessageDigest.getInstance("MD5");
			
			SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
			String currentDate = format.format(new java.sql.Timestamp((new java.util.Date()).getTime()));
			Map<String,Object> loginLog = new HashMap<String,Object>();
			loginLog.put("userId",userId);
			loginLog.put("operationDatetime",currentDate);
			
			conn = DBConn.getConnection();
			if(conn != null){
				List<WebserviceModel> pattern = Constant.GET_WEBSERVICE_MODEL(fileName,Constant.XML_KEY_WEBSERVICE_LOGIN + Constant.FIELD_GET);
				for(int i = 0 ; i < pattern.size() ; i++){	
					pattern.get(i).data =  map.get(pattern.get(i).key).toString();
					//System.err.println("data - " + pattern.get(i).key + " : "+ map.get(pattern.get(i).key));
				}
				
				String sqlXml = Constant.WEBSERVICE_GET_SQL(fileName,Constant.XML_KEY_WEBSERVICE_LOGIN + Constant.SQL);
				List<String> resultSet = Constant.GET_STRING_LIST(fileName,Constant.XML_KEY_WEBSERVICE_LOGIN + Constant.FIELD_GET_RESULTCOLUMN);
				//List<String> params = new ArrayList<String>();
				//List<String> params2 = new ArrayList<String>();
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				sql.append(sqlXml);
				for(WebserviceModel detail: pattern){
					String data = "";
					if("password".equalsIgnoreCase(detail.key)) {
						md.update(map.get(detail.key).toString().getBytes());
					    byte[] digest = md.digest();
					    //System.err.println(DatatypeConverter.printHexBinary(digest).toUpperCase());
					    data = DatatypeConverter.printHexBinary(digest).toUpperCase();
					}else {
						data = map.get(detail.key).toString();
					}
					sql.append(" AND "+ detail.table + "." +  detail.field + " = '" + data +"' ");
				}
				stmt = conn.createStatement();  
				//System.err.println(sql.toString());
		        rs = stmt.executeQuery(sql.toString());
		        BeanMap resultData = new BeanMap();
		        List<String> menuList = new ArrayList<String>();
		        while(rs.next()) {
		        	for(String key : resultSet){
		        		menuList.add(rs.getString(key));
		        		//resultData.put(key, rs.getString(key));
		        	}
		        }
		        if(menuList.size() > 0) {
		        	resultData.put("status", "true");
		        	loginLog.put("result", 1);
		        	
		        }else {
		        	resultData.put("status", "false");
		        	loginLog.put("result", 0);
		        	
		        }
		        insertData(Constant.WEBSERVICE_TRN_LOGIN_LOG_CONFIG_FILE_NAME,
	        			loginLog,
	        			userId,
	        			Constant.functionId.FUNCID_LOGIN);
		        resultData.put("menuList", menuList);
				result = JSON.encode(resultData);
				//result = "success";
			}else{
				result = "Connection Fail";
			}
			
		}catch (Exception e){
			LogWriter.error(e);
			result = e.toString();
			writeLog(Constant.functionId.FUNCID_LOGIN
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}
		/*writeLog(Constant.functionId.FUNCID_LOGIN,Constant.logField.LOG_TYPE_INFO
				   ,userId,Constant.logField.LOG_END);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_LOGIN
				,Constant.functionId.FUNCID_LOGIN,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_END);
		return result;
	}
	
	public String getPickingDetail(
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_PICKED,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_GET_PICKED
				,Constant.functionId.FUNCID_WEBSERVICE_GET_PICKED,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_START);
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			List<Map<String,Object>> locationList = new ArrayList<Map<String,Object>>();
			List<Map<String,Object>> candidateList = new ArrayList<Map<String,Object>>();
			List<Map<String,Object>> quotaList = new ArrayList<Map<String,Object>>();
			Map<String,Object> location = new HashMap<String,Object>();
			Map<String,Object> candidate = new HashMap<String,Object>();
			Map<String,Object> quota = new HashMap<String,Object>();
			String referenceNo = map.get("referenceNo").toString();
			BeanMap resultData = new BeanMap();
			if("DO1805-001".equalsIgnoreCase(referenceNo)){
				location.put("Location", "A1");
				location.put("Part No.", "4405A272");
				location.put("Qty", "120");
				locationList.add(location);
				location = new HashMap<String,Object>();
				location.put("Location", "A2");
				location.put("Part No.", "4405A273");
				location.put("Qty", "50");
				locationList.add(location);
				
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020013");
				candidate.put("Lot Date", "180402");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020014");
				candidate.put("Lot Date", "180402");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020015");
				candidate.put("Lot Date", "180402");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020016");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020017");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020018");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A272");
				candidate.put("Tag No.", "M411804020019");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "30");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A273");
				candidate.put("Tag No.", "M421804030001");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "50");
				candidateList.add(candidate);
				candidate = new HashMap<String,Object>();
				candidate.put("Part No.", "4405A273");
				candidate.put("Tag No.", "M421804030002");
				candidate.put("Lot Date", "180403");
				candidate.put("Qty", "50");
				candidateList.add(candidate);
				
				quota.put("Part No.", "4405A272");
				quota.put("Lot Date", "180402");
				quota.put("Quota Qty", "90");
				quotaList.add(quota);
				quota = new HashMap<String,Object>();
				quota.put("Part No.", "4405A272");
				quota.put("Lot Date", "180403");
				quota.put("Quota Qty", "30");
				quotaList.add(quota);
				quota = new HashMap<String,Object>();
				quota.put("Part No.", "4405A273");
				quota.put("Lot Date", "180403");
				quota.put("Quota Qty", "50");
				quotaList.add(quota);

				resultData.put("Result", "OK");
				resultData.put("ErrMsg", "");
			}else {
				resultData.put("Result", "NG");
				resultData.put("ErrMsg", "Cannot get Data");
			}
			
			resultData.put("referenceNo",referenceNo);
			resultData.put("locationList",locationList);
			resultData.put("candidateList",candidateList);
			resultData.put("quotaList",quotaList);
			result = JSON.encode(resultData);
		}catch (Exception e){
			LogWriter.error(e);
			result = e.toString();
			writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_PICKED
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		}
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_PICKED,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_END);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_GET_PICKED
				,Constant.functionId.FUNCID_WEBSERVICE_GET_PICKED,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_END);
		return result;
	}
	
	public String getShippingDetail(
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_GET_SHIPPING
				,Constant.functionId.FUNCID_WEBSERVICE_GET_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_START);
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			List<Map<String,Object>> pickedItemList = new ArrayList<Map<String,Object>>();
			Map<String,Object> pickedItem = new HashMap<String,Object>();
			String doCode = map.get("doCode").toString();
			BeanMap resultData = new BeanMap();
			if("DO1805-001".equalsIgnoreCase(doCode)){
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020013");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020014");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020015");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804030016");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A273");
				pickedItem.put("Tag No.", "M421804030001");
				pickedItem.put("Qty", "50");
				pickedItemList.add(pickedItem);
				resultData.put("Result", "OK");
				resultData.put("ErrMsg", "");
				resultData.put("santen_flag","1");
			}else if("DO1805-002".equalsIgnoreCase(doCode)){
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020013");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020014");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804020015");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A272");
				pickedItem.put("Tag No.", "M411804030016");
				pickedItem.put("Qty", "30");
				pickedItemList.add(pickedItem);
				pickedItem = new HashMap<String,Object>();
				pickedItem.put("Part No.", "4405A273");
				pickedItem.put("Tag No.", "M421804030001");
				pickedItem.put("Qty", "50");
				pickedItemList.add(pickedItem);
				resultData.put("Result", "OK");
				resultData.put("ErrMsg", "");
				resultData.put("santen_flag","0");
			}else if("DO1805-003".equalsIgnoreCase(doCode)){
			
				resultData.put("Result", "NG");
				resultData.put("ErrMsg", "Not fix price for this DO");
				resultData.put("santen_flag","1");
			}else {
				resultData.put("Result", "NG");
				resultData.put("ErrMsg", "Cannot get Data");
			}
			
			resultData.put("doCode",doCode);
			resultData.put("pickedItemList",pickedItemList);
			result = JSON.encode(resultData);
		}catch (Exception e){
			LogWriter.error(e);
			result = e.toString();
			writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_SHIPPING
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		}
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_GET_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_END);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_GET_SHIPPING
				,Constant.functionId.FUNCID_WEBSERVICE_GET_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_END);
		return result;
	}
	
	public String insertShippingDetail(
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_SHIPPING
				,Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_START);
		
		String fileName = Constant.functionId.mappingFunction.get(Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING);
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			String doCode = map.get("doCode").toString();
			List<Map<String,Object>> shippingList = (List<Map<String, Object>>) map.get("shippingList");
			List<Map<String,Object>> operationLog = (List<Map<String, Object>>) map.get("logData");
			for(Map<String,Object> shippingObject : shippingList) {
				shippingObject.put("referenceNo", doCode);
				shippingObject.put("resultMode", 2);
			}
			//INSERT OPERATION LOG
			insertListData(Constant.WEBSERVICE_TRN_OPER_LOG_CONFIG_FILE_NAME,operationLog,userId,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED);
			result = insertListData(fileName,shippingList,userId,Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING);
			
		}catch(Exception e){
			insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_SHIPPING
					,Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING,Constant.logField.LOG_TYPE_ERROR
					,e.toString());
			return e.toString();
		}
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_END);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_SHIPPING
				,Constant.functionId.FUNCID_WEBSERVICE_INSERT_SHIPPING,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_END);
		return result;
	}
	
	public String insertPickingDetail(
			@WebParam(name = "object") String object,
			@WebParam(name = "userId") String userId) {
		String result = "";
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_START);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_PICKED
				,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_START);
		
		String fileName = Constant.functionId.mappingFunction.get(Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED);
		try{
			ObjectMapper mapper = new ObjectMapper();
			Map<String,Object> map = mapper.readValue(object, Map.class);
			String referenceNo = map.get("referenceNo").toString();
			List<Map<String,Object>> pickingList = (List<Map<String, Object>>) map.get("pickingList");
			List<Map<String,Object>> operationLog = (List<Map<String, Object>>) map.get("logData");
			for(Map<String,Object> pickingObject : pickingList) {
				pickingObject.put("referenceNo", referenceNo);
				pickingObject.put("resultMode", 1);
			}
			//INSERT OPERATION LOG
			insertListData(Constant.WEBSERVICE_TRN_OPER_LOG_CONFIG_FILE_NAME,operationLog,userId,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED);
			result = insertListData(fileName,pickingList,userId,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED);
			
		}catch(Exception e){
			insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_PICKED
					,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED,Constant.logField.LOG_TYPE_ERROR
					,e.toString());
			return e.toString();
		}
		/*writeLog(Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED,Constant.logField.LOG_TYPE_INFO
				,userId,Constant.logField.LOG_END);*/
		insertInterfaceLog(userId,Constant.functionId.FUNCNM_WEBSERVICE_INSERT_PICKED
				,Constant.functionId.FUNCID_WEBSERVICE_INSERT_PICKED,Constant.logField.LOG_TYPE_INFO
				,Constant.logField.LOG_END);
		return result;
	}
	
	private void insertInterfaceLog(String userId, String functionName, String functionId, String logType, String logDescription){

		SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		String currentDate = format.format(new java.sql.Timestamp((new java.util.Date()).getTime()));
		String fileName = Constant.WEBSERVICE_TRN_INF_LOG_CONFIG_FILE_NAME;
		Map<String,Object> object = new HashMap<String,Object>();
		object.put("userId",userId);
		object.put("operationDatetime",currentDate);
		object.put("wsName",functionName);
		object.put("wsFunction",functionId);
		object.put("logType",logType);
		object.put("logDescription",logDescription);
		
		//insertData(fileName,log,userId,functionId);
		Connection conn = null;
		Statement stmt = null;  
	    ResultSet rs = null;  
		// Search result
		try{
			conn = DBConn.getConnection();
			if(conn != null){
					
				List<WebserviceModel> pattern = Constant.GET_WEBSERVICE_MODEL(fileName,Constant.XML_KEY_WEBSERVICE_FIELD_INSERT);
				for(int i = 0 ; i < pattern.size() ; i++){
					pattern.get(i).data =  object.get(pattern.get(i).key).toString();
					//System.err.println("data - " + pattern.get(i).key + " : "+ object.get(pattern.get(i).key));
				}
				
				List<String> tabelList = Constant.GET_STRING_LIST(fileName,Constant.XML_KEY_WEBSERVICE_TABLE_LIST);
				//List<String> params = new ArrayList<String>();
				//List<String> params2 = new ArrayList<String>();
				StringBuilder sql = new StringBuilder();
				StringBuilder sql2 = new StringBuilder();
				
				for(String tableName : tabelList){
					sql = new StringBuilder();
					sql2 = new StringBuilder();
					sql.append("INSERT INTO "+ tableName+" (");
					sql2.append("VALUES(");
					
					int i = 0;
					for(WebserviceModel detail: pattern){
						if(tableName.equalsIgnoreCase(detail.table)){
							if(!"1".equals(detail.endFlag)) {
								sql.append(" "+ detail.field +" ,");
								sql2.append(" '" + detail.data + "' ,");
							}else {
								sql.append(" "+ detail.field +" ");
								sql2.append(" '" + detail.data + "' ");
							}
							
						}
					}
					
					sql.append(")");
					sql2.append(")");
					
					sql.append(sql2.toString());
					
					stmt = conn.createStatement();  
			        int status = stmt.executeUpdate(sql.toString());
			        conn.commit();
				}
				
				
			}
		}catch (Exception e){
			LogWriter.error(e);
			writeLog(functionId
					,Constant.logField.LOG_TYPE_ERROR
					,userId
					,setString(e.toString()));
		} finally{
			if (conn != null){
				DBConn.closeConnection(conn);
			}
		}	
	}
}