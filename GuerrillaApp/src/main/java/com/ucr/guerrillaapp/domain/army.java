package com.ucr.guerrillaapp.domain;

public class army {
	private int assault;
    private int engineer;
    private int tank;
	public army(int assault, int engineer, int tank) {
		super();
		this.assault = assault;
		this.engineer = engineer;
		this.tank = tank;
	}
	public army() {
		super();
	}
	public int getAssault() {
		return assault;
	}
	public void setAssault(int assault) {
		this.assault = assault;
	}
	public int getEngineer() {
		return engineer;
	}
	public void setEngineer(int engineer) {
		this.engineer = engineer;
	}
	public int getTank() {
		return tank;
	}
	public void setTank(int tank) {
		this.tank = tank;
	}
	@Override
	public String toString() {
		return "{assault=" + assault + ", engineer=" + engineer + ", tank=" + tank + "}";
	}
    
}
