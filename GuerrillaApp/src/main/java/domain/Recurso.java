package domain;

public class Recurso {
	private int idRecurso;
	private int cantidadRecurso;
	private String nombreRecurso;

	public Recurso(int idRecurso, int cantidadRecurso, String nombreRecurso) {
		super();
		setIdRecurso(idRecurso);
		setNombreRecurso(nombreRecurso);
		setCantidadRecurso(cantidadRecurso);
	}

	public int getIdRecurso() {
		return idRecurso;
	}

	public void setIdRecurso(int idRecurso) {
		this.idRecurso = idRecurso;
	}

	public int getCantidadRecurso() {
		return cantidadRecurso;
	}

	public void setCantidadRecurso(int cantidadRecurso) {
		this.cantidadRecurso = cantidadRecurso;
	}

	public String getNombreRecurso() {
		return nombreRecurso;
	}

	public void setNombreRecurso(String nombreRecurso) {
		this.nombreRecurso = nombreRecurso;
	}

	@Override
	public String toString() {
		return "Recurso [idRecurso=" + idRecurso + ", cantidadRecurso=" + cantidadRecurso + ", nombreRecurso="
				+ nombreRecurso + "]";
	}

}
