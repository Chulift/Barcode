
package th.co.nssol.services.model.logic.jaxws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

/**
 * This class was generated by Apache CXF 2.7.7
 * Mon Jun 11 10:27:47 ICT 2018
 * Generated source version: 2.7.7
 */

@XmlRootElement(name = "insertPOResponse", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "insertPOResponse", namespace = "http://logic.model.services.nssol.co.th/")

public class InsertPOResponse {

    @XmlElement(name = "return")
    private java.lang.String _return;

    public java.lang.String getReturn() {
        return this._return;
    }

    public void setReturn(java.lang.String new_return)  {
        this._return = new_return;
    }

}

