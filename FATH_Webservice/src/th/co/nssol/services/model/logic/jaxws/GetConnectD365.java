
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

@XmlRootElement(name = "getConnectD365", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "getConnectD365", namespace = "http://logic.model.services.nssol.co.th/")

public class GetConnectD365 {

    @XmlElement(name = "userId")
    private java.lang.String userId;

    public java.lang.String getUserId() {
        return this.userId;
    }

    public void setUserId(java.lang.String newUserId)  {
        this.userId = newUserId;
    }

}
