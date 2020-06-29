package com.ucr.guerrillaapp.domain;

public class buildings {
	private int bunkers;

	public buildings(int bunkers) {
		super();
		this.bunkers = bunkers;
	}

	public buildings() {
		super();
	}

	public int getBunkers() {
		return bunkers;
	}

	public void setBunkers(int bunkers) {
		this.bunkers = bunkers;
	}

	@Override
	public String toString() {
		return "{bunkers=" + bunkers + "}";
	}
	
}
