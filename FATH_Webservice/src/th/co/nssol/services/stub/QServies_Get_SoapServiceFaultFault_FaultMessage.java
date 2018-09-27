
/**
 * QServies_Get_SoapServiceFaultFault_FaultMessage.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 13, 2008 (05:03:35 LKT)
 */

package th.co.nssol.services.stub;

public class QServies_Get_SoapServiceFaultFault_FaultMessage extends java.lang.Exception{
    
    private th.co.nssol.services.stub.QServicesGroupStub.FaultE faultMessage;
    
    public QServies_Get_SoapServiceFaultFault_FaultMessage() {
        super("QServies_Get_SoapServiceFaultFault_FaultMessage");
    }
           
    public QServies_Get_SoapServiceFaultFault_FaultMessage(java.lang.String s) {
       super(s);
    }
    
    public QServies_Get_SoapServiceFaultFault_FaultMessage(java.lang.String s, java.lang.Throwable ex) {
      super(s, ex);
    }
    
    public void setFaultMessage(th.co.nssol.services.stub.QServicesGroupStub.FaultE msg){
       faultMessage = msg;
    }
    
    public th.co.nssol.services.stub.QServicesGroupStub.FaultE getFaultMessage(){
       return faultMessage;
    }
}
    