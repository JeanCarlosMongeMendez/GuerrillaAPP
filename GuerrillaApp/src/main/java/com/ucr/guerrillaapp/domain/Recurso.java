package com.ucr.guerrillaapp.domain;

public class Recurso {
	private int idRecurso;
	private int quantity;
	private String resource;

	public Recurso(int idRecurso, int quantity, String resource) {
		super();
		setIdRecurso(idRecurso);
		setResource(resource);
		setQuantity(quantity);
	}

	public Recurso() {
		// TODO Auto-generated constructor stub
	}

	public int getIdRecurso() {
		return idRecurso;
	}

	public void setIdRecurso(int idRecurso) {
		this.idRecurso = idRecurso;
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
		return "Recurso [idRecurso=" + idRecurso + ", quantity=" + quantity + ", resource="
				+ resource + "]";
	}

}
