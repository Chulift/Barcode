package th.co.nssol.services.model.logic;

import java.util.ArrayList;
import java.util.List;

import javax.jws.WebParam;
import javax.jws.WebService;

import org.apache.axis2.AxisFault;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.commons.httpclient.Header;


import th.co.nssol.services.stub.MyAuth;
import th.co.nssol.services.stub.QServicesGroupStub;
import th.co.nssol.services.stub.QServicesGroupStub.CallContext;
import th.co.nssol.services.stub.QServicesGroupStub.CallContextE;
import th.co.nssol.services.stub.QServicesGroupStub.Get;
import th.co.nssol.services.stub.QServicesGroupStub.GetResponse;
import th.co.nssol.services.stub.RoutingServiceStub;
import th.co.nssol.services.stub.RoutingServiceStub.StudentServiceGetStudentRequest;

public class WebserviceConnect {
	
	public WebserviceConnect(){
		
	}
	
	public String getD365() throws Exception {
		
		TokenService tokenService = new TokenService();
		String token = tokenService.getAccessToken();
		
		/*String store = "C:\\Program Files\\Java\\jdk1.8.0_162\\lib\\security\\cacerts";
		System.setProperty("-Djavax.net.ssl.trustStore", store);
		System.setProperty("-Djavax.net.ssl.trustStorePassword", "password@1");
		System.setProperty("trustStore", store);
		System.setProperty("trustStorePassword", "password@1");
		System.setProperty("javax.net.ssl.trustStore", store);
		System.setProperty("javax.net.ssl.trustStorePassword", "password@1");*/
		
		QServicesGroupStub qServicesGroupStub = new QServicesGroupStub(
				//"http://presales-pc:8080/kch/services/IMWProcessService/apply");
				"https://usnconeboxax1aos.cloud.onebox.dynamics.com/soap/services/QServicesGroup");
//		qServicesGroupStub._setServiceClient(MyAuth.generateAuth(qServicesGroupStub
//				._getServiceClient()))
		List headerList = new ArrayList();
		Header header = new Header();
		header.setName(HTTPConstants.HEADER_AUTHORIZATION);
		header.setValue("Bearer " + token);
		headerList.add(header);
		qServicesGroupStub._getServiceClient().getOptions().setProperty(HTTPConstants.HTTP_HEADERS, headerList);

		Get get = new Get();
		GetResponse getResponse  = new GetResponse();
		CallContextE callContext0 = new CallContextE();
		CallContext callContext = new CallContext();

		callContext0.setCallContext(callContext);
		try{
			getResponse = qServicesGroupStub.Get(get, callContext0);
			System.err.println("SUCCESS "+ getResponse.getResult().getQtestContract()[0].getParmField1());
			
		}catch(Exception e){
			System.err.println("ERROR "+ e.toString()); 
			System.err.println("ERROR "+ getResponse.getResult().getQtestContract()); 
		}
		
		return getResponse.getResult().getQtestContract()[0].getParmField1()+"";
	}
	
}
