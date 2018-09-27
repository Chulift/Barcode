
/**
 * StudentService_UpdateStudent_AifFaultFault_FaultMessage.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 13, 2008 (05:03:35 LKT)
 */

package th.co.nssol.services.stub;

public class StudentService_UpdateStudent_AifFaultFault_FaultMessage extends java.lang.Exception{
    
    private th.co.nssol.services.stub.RoutingServiceStub.AifFaultE faultMessage;
    
    public StudentService_UpdateStudent_AifFaultFault_FaultMessage() {
        super("StudentService_UpdateStudent_AifFaultFault_FaultMessage");
    }
           
    public StudentService_UpdateStudent_AifFaultFault_FaultMessage(java.lang.String s) {
       super(s);
    }
    
    public StudentService_UpdateStudent_AifFaultFault_FaultMessage(java.lang.String s, java.lang.Throwable ex) {
      super(s, ex);
    }
    
    public void setFaultMessage(th.co.nssol.services.stub.RoutingServiceStub.AifFaultE msg){
       faultMessage = msg;
    }
    
    public th.co.nssol.services.stub.RoutingServiceStub.AifFaultE getFaultMessage(){
       return faultMessage;
    }
}
    