package th.co.nssol.services.model.logic;

import java.util.ArrayList;
import java.util.List;

import javax.jws.WebParam;
import javax.jws.WebService;

import org.apache.axis2.AxisFault;
import org.apache.axis2.context.ConfigurationContext;
import org.apache.axis2.engine.AxisConfiguration;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.commons.httpclient.Header;


import th.co.nssol.services.stub.MyAuth;
import th.co.nssol.services.stub.RoutingServiceStub;
import th.co.nssol.services.stub.RoutingServiceStub.CallContext;
import th.co.nssol.services.stub.RoutingServiceStub.CallContextE;
import th.co.nssol.services.stub.RoutingServiceStub.StudentServiceGetStudentRequest;
import th.co.nssol.services.stub.RoutingServiceStub.StudentServiceGetStudentsRequest;
import th.co.nssol.services.stub.RoutingServiceStub.StudentServiceGetStudentsResponse;

public class WebserviceConnectAX {
	
	public WebserviceConnectAX(){
		
	}
	
	public String getAX() throws Exception {
		
		RoutingServiceStub routingServiceStub = new RoutingServiceStub();
		
		routingServiceStub._setServiceClient(MyAuth.generateAuth(routingServiceStub
				._getServiceClient()));
		
		
				
		CallContextE callContext = new CallContextE();
		StudentServiceGetStudentsResponse result = new StudentServiceGetStudentsResponse();
		StudentServiceGetStudentsRequest studentServiceGetStudentsRequest = new StudentServiceGetStudentsRequest();
		try {
			result = routingServiceStub.GetStudents(studentServiceGetStudentsRequest, callContext);
			
		}catch(Exception E) {
			System.out.println(E.toString());
		}
		
		
		return result.getResponse().getStudentInfo()[0].getFirstName();
	}
}
