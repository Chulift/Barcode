package th.co.nssol.common.util;

public interface ConstantServices {
	public interface Location {
		public final String Store_B = "Store B";
		public final String ShipmentStore = "Shipment Store";
	}
	
	public interface ControlType {
		public final String FIFO = "FIFO";
		public final String FEFO = "FEFO";	
	}
	
	public interface TransactionType {
		public final String Receive = "REC";									
		public final String Issue = "ISS";									
		public final String Return = "RET";									
		public final String LocationIssue = "LCI";							
		public final String LocationReceive = "LCR";
		public final String StockAdjustIn = "STI";									
		public final String StockAdjustOut = "STO";									
		public final String Reject = "REJ";
		public final String Consumtion = "CON";
	}
	
	public interface CompanyType {
		public final String Owner = "01";						
		public final String Customer = "02";								
		public final String Supplier = "03";							
 
	}
	
	public interface ReceiveType {
		public final String Receive = "REC";
		public final String Return = "RET";
	}
	
	public interface IssueType {
		public final String Issue = "ISS";
	}
	
	public interface StockTakingStatus {
		public final String STOCKTAKINGSTATUS_STARTED = "S";
		public final String STOCKTAKINGSTATUS_FINISHED = "F";
	}
	
	public interface StockTakingType {
		public final String TAKINGTYPE_LOCFOUND  = "LOC";
		public final String TAKINGTYPE_NORMAL = "NOR";
		public final String TAKINGTYPE_QTYNOTEQUAL = "QTY";
		public final String TAKINGTYPE_PHYFOUND  = "PHY";
	}
}
