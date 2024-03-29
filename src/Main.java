import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

//FIXME ENTER bricht Bearbeitungsvorgang nicht ab
//FIXME Feste Nummer an Datenbankeintraegen

public class Main {
	// Amount
	private static short maxLTB = 541;
	private static short countLTB;
	private static int lastLTB ;

	// Cache
	private static String dataset;
	private static String menu;
	private static boolean stop = false;

	// Path
	static String pathCSV = ".\\data\\" + "LTB.csv";
	static String pathTXT = ".\\data\\" + "LTB.txt";

	// Open Scanner
	static Scanner scanner = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println("Verwaltung für Lustige Taschenbücher");
		System.out.println("(C) 2021 Leon Niklas Kelle");
		System.out.println();

		// Read stored Information
		read();

		// Count LTBs
		counter();
		System.out.println("Es werden aktuell " + countLTB + " LTBs verwaltet.");
		
		// Mark last
		last();

		System.out.println("Tippe '0' oder 'exit' um zu beenden!");
		System.out.println();

		// Start Progress
		boolean loop = true;
		while (loop) {
			System.out.print("LTB>");
			menu = scanner.next();

			// Exit progress
			if (menu.equals("exit") || menu.equals("0")) {
				write();
				scanner.close();
				loop = false;
				System.exit(0);
			}

			// Show Item
			else {
				dataset = LTB.get(Integer.parseInt(menu));
				String parseLine[] = dataset.split(";");

				// Reformat null values
				if (parseLine[2].equals("null")) {
					parseLine[2] = "";
				}
				if (parseLine[3].equals("null")) {
					parseLine[3] = "";
				}

				System.out.println("Nummer: \t" + parseLine[0]);
				System.out.println("Besitz: \t" + parseLine[1]);

				// Show info if owned
				if (Boolean.parseBoolean(parseLine[1])) {
					System.out.println("Zustand: \t" + parseLine[2]);
					System.out.println("Kommentar: \t" + parseLine[3]);
				}

				System.out.println();
				System.out.print("Datensatz bearbeiten? (J/N)");
				menu = scanner.next();
				System.out.println();

				// Edit values
				if (menu.equals("J") || menu.equals("j")) {	
					System.out.println("Zum belassen einer Variable 'null' tippen.");
					System.out.println();
					
					// Show info
					System.out.print("Besitz: \t");
					String newValue1 = scanner.next();
					if (!newValue1.equals("")) {
						parseLine[1] = newValue1;
					}

					// Only edit if owned
					if (Boolean.parseBoolean(parseLine[1])) {
						System.out.print("Zustand: \t");
						String newValue3 = scanner.next();
						if (!newValue3.equals("")) {
							parseLine[2] = newValue3;
						}
						System.out.print("Kommentar: \t");
						String newValue4 = scanner.next();
						if (!newValue4.equals("")) {
							parseLine[3] = newValue4;
						}
					}

					// Delete values if not owned
					else {
						parseLine[2] = null;
						parseLine[3] = null;
					}

					System.out.println();

					LTB.set(Integer.parseInt(parseLine[0]), Boolean.parseBoolean(parseLine[1]), parseLine[2],
							parseLine[3]);
				}
			}
		}
	}

	public static void counter() {
		for (int i = 1; i < LTB.size(); i++) {
			String parseLine[] = LTB.get(i).split(";");
			if (Boolean.parseBoolean(parseLine[1])) {
				countLTB++;
				lastLTB = i;
			}
		}
	}
	
	public static void last() {
		dataset = LTB.get(lastLTB);
		String parseLine[] = dataset.split(";");
		parseLine[3] = "Letztes Buch";
		LTB.set(lastLTB, true, parseLine[2], parseLine[3]);
	}

	public static void read() {		
		// Create Entries
		for (int i = 0; i < maxLTB + 1; i++) {
			new LTB(i, false, null, null);
		}

		// Fill Entries
		try (FileReader fileReader = new FileReader(pathCSV);
				BufferedReader bufferedReader = new BufferedReader(fileReader);) {
			String line = bufferedReader.readLine(); // First line
			while (line != null) {
				String parseLine[] = line.split(";");
				if (parseLine[3].equals("Letzes Buch")) {
					parseLine[3] = "null";
				}
				LTB.set(Integer.parseInt(parseLine[0]), Boolean.parseBoolean(parseLine[1]), parseLine[2], parseLine[3]);
				line = bufferedReader.readLine();
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public static void write() {
		// Write CSV file
		try (FileWriter fileWriter = new FileWriter(pathCSV);
				BufferedWriter bufferedWriter = new BufferedWriter(fileWriter)) {
			for (int i = 0; i < LTB.size(); i++) {
				bufferedWriter.write(LTB.get(i));
				bufferedWriter.newLine();
			}
			bufferedWriter.flush();
			bufferedWriter.close();
		} catch (IOException e) {
			e.printStackTrace();
		}

		// Write TXT file
		try (FileWriter fileWriter = new FileWriter(pathTXT);
				BufferedWriter bufferedWriter = new BufferedWriter(fileWriter)) {

			// Write header
			bufferedWriter.write(" Nr. | ");
			bufferedWriter.write("Besitz \t");
			bufferedWriter.write("Zustand \t");
			bufferedWriter.write("Kommentar ");
			bufferedWriter.newLine();

			// Read information
			for (int i = 1; i < LTB.size(); i++) {
				dataset = LTB.get(i);
				String parseLine[] = dataset.split(";");

				// Reformat values
				if (parseLine[1].equals("false")) {
					parseLine[1] = "Nein";
				}
				if (parseLine[1].equals("true")) {
					parseLine[1] = "Ja";
				}
				if (parseLine[2].equals("null")) {
					parseLine[2] = "----";
				}
				if (parseLine[3].equals("null")) {
					parseLine[3] = "---";
				}

				// Convert numbers to format
				String id = String.valueOf(i);
				if (id.length() == 1) {
					id = "00" + id;
				} else if (id.length() == 2) {
					id = "0" + id;
				}

				// Save if relevant
				if (!stop) {
					if (parseLine[1].equals("Nein") || !parseLine[3].equals("---")) {
						bufferedWriter.write(" " + id + " | ");
						bufferedWriter.write(parseLine[1] + "\t");
						bufferedWriter.write(parseLine[2] + "\t\t");
						bufferedWriter.write(parseLine[3]);
						bufferedWriter.newLine();
					}
					if (parseLine[3].equals("Letztes Buch")) {
						stop = true;
					}
				}
				
			}
			bufferedWriter.flush();
			bufferedWriter.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
