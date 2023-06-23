const API_PERSONAS_URL = 'https://localhost:7075/api/personas/getPersonas'
//'https://localhost:7008/api/personas/getPersonas' // link de obtener todas las personas

function listaPersonas() {
    fetch(API_PERSONAS_URL)
        .then((respuesta) => respuesta.json())
        .then((respuesta) => {
            if (!respuesta.ok) {
                alert("ERROR!")
                return
            }

            const cuerpoTabla = document.querySelector('tbody')

            // nombre de la lista en swagger
            respuesta.listPersonas.forEach((per) => {
                const fila = document.createElement('tr')
                fila.innerHTML += `<td>${per.id}</td>` // filas de la lista
                fila.innerHTML += `<td>${per.nombre}</td>`
                fila.innerHTML += `<td>${per.apellido}</td>`

                cuerpoTabla.append(fila)
            });


        }).catch((err)=>{
            alert("No funciono")
        })
    

}