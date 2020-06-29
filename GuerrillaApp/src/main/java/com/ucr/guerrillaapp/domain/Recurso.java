package com.ucr.guerrillaapp.domain;

public class Recurso {
	
	private int quantity;
	private String resource;

	public Recurso( int quantity, String resource) {
		super();
		
		setResource(resource);
		setQuantity(quantity);
	}

	public Recurso() {
		// TODO Auto-generated constructor stub
	}


	public int getQuantity() {
		return quantity;
	}

	public void setQuantity(int quantity) {
		this.quantity = quantity;
	}

	public String getResource() {
		return resource;
	}

	public void setResource(String resource) {
		this.resource = resource;
	}

	@Override
	public String toString() {
		return "Recurso [quantity=" + quantity + ", resource="
				+ resource + "]";
	}

}
