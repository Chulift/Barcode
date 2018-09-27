package th.co.nssol.common.util;

public class ConsMessage {

	/*
	 * When insert new Variable please remove whitespace like " ERR_0001 " to
	 * "ERR_001" because it will cause of error in program
	 */
	static public final String INF_COMPLETED = "INF_0001";
	static public final String ERR_CRITICALSYSTEM = "ERR_0001";
	static public final String ERR_INCORRECTOPR = "ERR_0002";
	static public final String ERR_INCORRECTLOC = "ERR_0003";
	static public final String ERR_INCORRECTLABEL = "ERR_0004";
	static public final String ERR_LABELALREADYINSTOCK= "ERR_0005";
	static public final String ERR_DATADOESNOTEXIST = "ERR_0006";
	static public final String ERR_PLSSTARTSTOCKTAKING = "ERR_0007";
	static public final String ERR_LABELIDNOTEXIST = "ERR_0008";
	
	static public final String ERR_INVALIDQTY = "ERR_0009";
	static public final String ERR_DATANOTFOUND = "ERR_0010";
	static public final String ERR_CANNOTPICKING = "ERR_0011";
	static public final String ERR_PARTIDINPICKING = "ERR_0012";
	static public final String ERR_PARTIDEXISTNG = "ERR_0013";
	static public final String ERR_PARTIDNOTEXISTFG = "ERR_0014";
    static public final String ERR_INCORRECTLABELID	= "ERR_0015";
    static public final String ERR_LABELIDEXIST	= "ERR_0016";
    static public final String ERR_LABELNOTINSTOCK	= "ERR_0017";
    static public final String 	ERR_LABELNOTPICKING	= "ERR_0018";
    static public final String 	ERR_RESULTALREADYOK	= "ERR_0019";
    static public final String 	ERR_RESULTALREADYNG	= "ERR_0020";
    static public final String 	ERR_RESULTALREADYHOLD	= "ERR_0021";
    static public final String 	ERR_RESULTEXIST	= "ERR_0022";
    static public final String 	ERR_PARTIDNOTPICKING	= "ERR_0023";
    static public final String 	ERR_PRODUCTCODECHANGED	= "ERR_0024";
    static public final String 	ERR_CANNOTRETURNPART	= "ERR_0025";
    static public final String 	ERR_CANNOTOPERATION	= "ERR_0026";
    
    /** revise 11/03/2015, error message **/
    static public final String ERR_CSV_FILE_NOT_EXISTED = "ERR_0027";
    static public final String ERR_TOTAL_COLUMNINCORRECT = "ERR_0028";
    static public final String ERR_INVALID_INTERFACE_NO = "ERR_0029";
    static public final String ERR_INVALID_CSV_FORMAT = "ERR_0030";
    static public final String ERR_INVALID_TO_LOCATION_CODE = "ERR_0031";
    static public final String ERR_LABEL_CODE_ISSUED_ALREADY = "ERR_0032";
    static public final String ERR_LABEL_CODE_NOT_EXISTED_INSTOCK = "ERR_0033";
    static public final String ERR_INVALID_FROM_LOCATION_CODE = "ERR_0034";
    static public final String ERR_FROM_TO_LOCATION_CANNOT_SAME_LOCATION = "ERR_0035";
    static public final String ERR_INVALID_FROM_AND_TO_LOCATIONCODE = "ERR_0036";
    
}
