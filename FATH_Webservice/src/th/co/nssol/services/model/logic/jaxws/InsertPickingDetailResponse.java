
package th.co.nssol.services.model.logic.jaxws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

/**
 * This class was generated by Apache CXF 3.2.4
 * Tue Jul 31 10:56:31 ICT 2018
 * Generated source version: 3.2.4
 */

@XmlRootElement(name = "insertPickingDetailResponse", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "insertPickingDetailResponse", namespace = "http://logic.model.services.nssol.co.th/")

public class InsertPickingDetailResponse {

    @XmlElement(name = "return")
    private java.lang.String _return;

    public java.lang.String getReturn() {
        return this._return;
    }

    public void setReturn(java.lang.String new_return)  {
        this._return = new_return;
    }

}

