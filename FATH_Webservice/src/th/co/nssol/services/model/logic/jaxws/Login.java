
package th.co.nssol.services.model.logic.jaxws;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

/**
 * This class was generated by Apache CXF 3.2.4
 * Tue Jul 24 17:44:49 ICT 2018
 * Generated source version: 3.2.4
 */

@XmlRootElement(name = "login", namespace = "http://logic.model.services.nssol.co.th/")
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "login", namespace = "http://logic.model.services.nssol.co.th/")

public class Login {

    @XmlElement(name = "object")
    private java.lang.String object;

    public java.lang.String getObject() {
        return this.object;
    }

    public void setObject(java.lang.String newObject)  {
        this.object = newObject;
    }

}
