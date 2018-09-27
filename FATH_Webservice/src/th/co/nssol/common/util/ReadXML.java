package th.co.nssol.common.util;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import th.co.nssol.services.model.data.WebserviceModel;



public class ReadXML {

	public static String getValue(String filename, String key) {
		String value = null;
		String[] keys = null;
		DocumentBuilderFactory builderFactory = null;
		DocumentBuilder builder = null;
		Document document = null;
		NodeList list = null;
		Element element = null;
		NodeList valueList = null;
		File file = null;

		try {
			file = new File(filename);
			keys = key.split(":");
			builderFactory = DocumentBuilderFactory.newInstance();
			builderFactory.setIgnoringComments(true);
			builderFactory.setIgnoringElementContentWhitespace(true);
			builder = builderFactory.newDocumentBuilder();
			document = builder.parse(file);

			list = document.getElementsByTagName(keys[1]);

			for (int i = 0; i < list.getLength(); i++) {
				element = (Element) list.item(i);
				valueList = element.getElementsByTagName(keys[2]);
				if (valueList.item(0).getFirstChild() != null) {
					value = valueList.item(0).getFirstChild().getNodeValue();
				} else {
					value = null;
				}
			}

		} catch (Exception e) {
			// LogWriter.error("getValue() error in ReadXML class. FileName="
			// + filename + " Key=" + key, e);
		}
		return value;
	}
	
	public static List<WebserviceModel> getWebServiceValue(String filename, String key) {
		String value = null;
		String[] keys = null;
		DocumentBuilderFactory builderFactory = null;
		DocumentBuilder builder = null;
		Document document = null;
		NodeList list = null;
		Element element = null;
		NodeList valueList = null;
		File file = null;
		List<WebserviceModel> result = new ArrayList<WebserviceModel>();

		try {
			file = new File(filename);
			keys = key.split(":");
			builderFactory = DocumentBuilderFactory.newInstance();
			builderFactory.setIgnoringComments(true);
			builderFactory.setIgnoringElementContentWhitespace(true);
			builder = builderFactory.newDocumentBuilder();
			document = builder.parse(file);

			list = document.getElementsByTagName(keys[1]);

			for (int i = 0; i < list.getLength(); i++) {
				element = (Element) list.item(i);
				valueList = element.getElementsByTagName(keys[2]);
				
				for(int j =0; j < valueList.getLength(); j++){
					result.add(getWebserviceData(valueList.item(j)));
				}
				
			}

		} catch (Exception e) {
			// LogWriter.error("getValue() error in ReadXML class. FileName="
			// + filename + " Key=" + key, e);
		}
		return result;
	}
	
	
	private static WebserviceModel getWebserviceData(Node node) {
        //XMLReaderDOM domReader = new XMLReaderDOM();
		WebserviceModel model = new WebserviceModel();
        if (node.getNodeType() == Node.ELEMENT_NODE) {
            Element element = (Element) node;
            model.key = getTagValue("key", element);
            model.table = getTagValue("table", element);
            model.field = getTagValue("field", element);
            model.endFlag = getTagValue("endFlag", element);
        }

        return model;
    }
	
	public static List<String> getStringListValue(String filename, String key) {
		String value = null;
		String[] keys = null;
		DocumentBuilderFactory builderFactory = null;
		DocumentBuilder builder = null;
		Document document = null;
		NodeList list = null;
		Element element = null;
		NodeList valueList = null;
		NodeList valueSecondList = null;
		File file = null;
		List<String> result = new ArrayList<String>();

		try {
			file = new File(filename);
			keys = key.split(":");
			builderFactory = DocumentBuilderFactory.newInstance();
			builderFactory.setIgnoringComments(true);
			builderFactory.setIgnoringElementContentWhitespace(true);
			builder = builderFactory.newDocumentBuilder();
			document = builder.parse(file);

			list = document.getElementsByTagName(keys[1]);

			for (int i = 0; i < list.getLength(); i++) {
				element = (Element) list.item(i);
				valueList = element.getElementsByTagName(keys[2]);
				
				for(int j =0; j < valueList.getLength(); j++){
					element = (Element) valueList.item(j);
					valueSecondList = element.getElementsByTagName(keys[3]);
					
					for(int k =0; k < valueSecondList.getLength(); k++){
						
						result.add(valueSecondList.item(k).getFirstChild().getNodeValue());
					}
					
				}
				
			}

		} catch (Exception e) {
			// LogWriter.error("getValue() error in ReadXML class. FileName="
			// + filename + " Key=" + key, e);
		}
		return result;
	}
	
	private static String getTagValue(String tag, Element element) {
        NodeList nodeList = element.getElementsByTagName(tag).item(0).getChildNodes();
        Node node = (Node) nodeList.item(0);
        return node != null ? node.getNodeValue() : "";
    }
}