import java.util.ArrayList;

public class LTB {
	// Create ArrayList
	static ArrayList<String> ltb = new ArrayList<String>();

	// Characteristics
	private int id;
	private boolean ownership;
	private String condition;
	private String comment;

	// Basic functions
	public static void remove(int id) {
		ltb.remove(id);	//unused
	}

	public static int size() {
		return ltb.size();
	}

	public static String get(int id) {
		return ltb.get(id);
	}

	public static void set(int id, boolean ownership, String condition, String comment) {
		ltb.set(id, id + ";" + ownership + ";" + condition + ";" + comment);
	}

	// Create Object
	public LTB(int id, boolean ownership, String condition, String comment) {
		super();
		this.id = id;
		this.ownership = ownership;
		this.condition = condition;
		this.comment = comment;

		ltb.add(id + ";" + ownership + ";" + condition + ";" + comment);
	}

	/* GETTERS & SETTERS */
	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public boolean isOwnership() {
		return ownership;
	}

	public void setOwnership(boolean ownership) {
		this.ownership = ownership;
	}

	public String getCondition() {
		return condition;
	}

	public void setCondition(String condition) {
		this.condition = condition;
	}

	public String getComment() {
		return comment;
	}

	public void setComment(String comment) {
		this.comment = comment;
	}
}
