package th.co.nssol.services.stub;

import java.util.ArrayList;
import java.util.List;

import org.apache.axis2.client.Options;
import org.apache.axis2.client.ServiceClient;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.axis2.transport.http.HttpTransportProperties;
import org.apache.commons.httpclient.auth.AuthPolicy;
import org.apache.cxf.rs.security.oauth2.tokens.bearer.BearerAccessToken;

import th.co.nssol.common.util.JCIFS_NTLMScheme;



public class MyAuth {
	/*private static final String XML_CONF_FILE_NAME = Constant.CONF_PATH_SYSTEM
			+ Constant.APPL_CONFIG_FILE_NAME;

	private static final String XML_KEY_HOST = "config:webservice:host";

	private static final String XML_KEY_DOMAIN = "config:webservice:domain";

	private static final String XML_KEY_USERNAME = "config:webservice:username";

	private static final String XML_KEY_PASSWORD = "config:webservice:password";

	private static final String WEBSERVICE_HOST = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_HOST);

	private static final String WEBSERVICE_DOMAIN = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_DOMAIN);

	private static final String WEBSERVICE_USERNAME = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_USERNAME);

	private static final String WEBSERVICE_PASSWORD = ReadXML.getValue(
			XML_CONF_FILE_NAME, XML_KEY_PASSWORD);*/

	public static ServiceClient generateAuth(ServiceClient m_ServiceClient)
			throws Exception {

		AuthPolicy.registerAuthScheme(AuthPolicy.NTLM, JCIFS_NTLMScheme.class);

		Options options = m_ServiceClient.getOptions();
		options.setTimeOutInMilliSeconds(2 * 60 * 1000);

		HttpTransportProperties.Authenticator basicauth = new HttpTransportProperties.Authenticator();
		// basicauth.setUsername("NAV_WS");
		// basicauth.setPassword("PASSW0rd");
		// basicauth.setDomain("PRESALES2-PC");
		// basicauth.setHost("presales2-pc.th.nssol.local.co.th");
		basicauth.setUsername("axsrv");
		basicauth.setPassword("password@1");
		basicauth.setDomain("th.nssol.local.co.th");
		basicauth.setHost("ax2012aos.th.nssol.local.co.th");
		List<String> authPrefs = new ArrayList<String>(1);

		authPrefs.add(AuthPolicy.NTLM);
		basicauth.setAuthSchemes(authPrefs);
		basicauth.setPreemptiveAuthentication(true);
		options.setProperty(HTTPConstants.AUTHENTICATE, basicauth);
		m_ServiceClient.setOptions(options);
		return m_ServiceClient;
	}
}
