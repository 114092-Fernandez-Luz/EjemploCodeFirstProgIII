function guardarPersona(){
    let txtNombre = document.getElementById("txtNombre")
    let txtApellido = document.getElementById("txtApellido")
    let txtIdCategoria = document.getElementById("idCategoria")

    if(txtNombre.value === ""){
        alert("EL NOMBRE ES OBLIGATORIO")
        return false;
    }
    if(txtApellido.value === ""){
        alert("EL APELLIDO ES OBLIGATORIO")
        return false;
    }
    if(txtIdCategoria.value === ""){
        alert("Debe ingresar uan categoria")
        return false;
    }

    const url = 'https://localhost:7008/api/personas/postNuevaPersona';
    //'https://localhost:7075/api/personas/postNuevaPersona'
   
    const request = {
            "nombre": txtNombre.value,
            "apellido": txtApellido.value,
            "idCategoria": txtIdCategoria.value
    }

    fetch(url,{
        body: JSON.stringify(request),
        method: "post",
        headers: {
            "Content-Type" : "application/json"
        }

    })
    .then(respuesta => respuesta.json())
    .then(respuesta =>{
        if(respuesta.ok){
            alert("Persona agregada con exito")
            localStorage.setItem("datoAMostrar", respuesta.listPersonas[0].nombre)
            window.location.replace("nuevo.html")
        }
        else{
            alert("Error al agregar persona")
        }
    })
    .catch(err => alert("ERROR: " + err));

}