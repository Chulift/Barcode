
package th.co.nssol.services.model.logic.jaxws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

/**
 * This class was generated by Apache CXF 2.7.7
 * Wed Jun 13 11:10:33 ICT 2018
 * Generated source version: 2.7.7
 */

@XmlRootElement(name = "insertCPO", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "insertCPO", namespace = "http://logic.model.services.nssol.co.th/", propOrder = {"object", "userId"})

public class InsertCPO {

    @XmlElement(name = "object")
    private java.lang.String object;
    @XmlElement(name = "userId")
    private java.lang.String userId;

    public java.lang.String getObject() {
        return this.object;
    }

    public void setObject(java.lang.String newObject)  {
        this.object = newObject;
    }

    public java.lang.String getUserId() {
        return this.userId;
    }

    public void setUserId(java.lang.String newUserId)  {
        this.userId = newUserId;
    }

}

