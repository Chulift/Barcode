
package th.co.nssol.services.model.logic.jaxws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

/**
 * This class was generated by Apache CXF 3.2.4
 * Thu Jul 26 09:52:47 ICT 2018
 * Generated source version: 3.2.4
 */

@XmlRootElement(name = "getPickingDetailResponse", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "getPickingDetailResponse", namespace = "http://logic.model.services.nssol.co.th/")

public class GetPickingDetailResponse {

    @XmlElement(name = "return")
    private java.lang.String _return;

    public java.lang.String getReturn() {
        return this._return;
    }

    public void setReturn(java.lang.String new_return)  {
        this._return = new_return;
    }

}

