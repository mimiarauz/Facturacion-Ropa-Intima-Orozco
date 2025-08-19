let estadoOriginalBtnBuscar;


document.getElementById('txtprecioventa').addEventListener('keypress', function (e) {
   const keyCode = e.which || e.keyCode;
   const inputValue = String.fromCharCode(keyCode);

   // Validar que solo se ingresen números y un punto decimal
   if (!/^\d+$/.test(inputValue) && inputValue !== '.') {
       e.preventDefault();
   }
});

// Agregar un manejador de eventos keypress para el campo de stock
document.getElementById('txtstock').addEventListener('keypress', function (e) {
   const keyCode = e.which || e.keyCode;
   const inputValue = String.fromCharCode(keyCode);

   // Validar que solo se ingresen números
   if (!/^\d+$/.test(inputValue)) {
       e.preventDefault();
   }
});

function desactivarTodoExceptoNuevo(desactivar) {
   console.log('Desactivando todo excepto Nuevo');
   // Desactivar campos de entrada
   const campos = document.querySelectorAll('.t');
   campos.forEach(campo => {
       campo.disabled = true;
   });

    const btnbuscar = document.getElementById('btnbuscar');
    btnbuscar.disabled = desactivar;

    // Guardar el estado original del botón de búsqueda la primera vez que se llama a la función
    if (typeof estadoOriginalBtnBuscar === 'undefined') {
        estadoOriginalBtnBuscar = btnbuscar.disabled;
    }

   // Desactivar botones "Guardar", "Actualizar" y "Cancelar"
   const botones = document.querySelectorAll('.b');
   botones.forEach(boton => {
       if (boton.value !== 'Nuevo') {
           boton.disabled = true;
           boton.style.opacity = '0.5';  // Puedes ajustar este valor según lo desees
           boton.style.cursor = 'not-allowed';
       } 
       else {
           // Activar botón "Nuevo"
           boton.disabled = false;
           boton.style.opacity = '1';  // Restaurar la opacidad a su valor original
           boton.style.cursor = 'pointer';
       }
   });
   console.log('Estado original del botón de búsqueda:', estadoOriginalBtnBuscar);
}

function activarNuevo() {
   console.log('Limpiando y desactivando');

   // Activar campos de entrada
   const campos = document.querySelectorAll('.t');
   campos.forEach(campo => {
       campo.disabled = false;
   });

   // Activar tabla (haciéndola interactiva)
   const tabla = document.getElementById('tablaproducto');
   tabla.style.pointerEvents = 'auto';

   // Activar botones "Guardar" y "Cancelar"
   const botonesGuardarCancelar = document.querySelectorAll('.b[value="Guardar"], .b[value="Cancelar"]');
   botonesGuardarCancelar.forEach(boton => {
       if (boton.value === 'Guardar' || boton.value === 'Cancelar') {
           boton.disabled = false;
           boton.style.opacity = '1';  // Restaurar la opacidad a su valor original
           boton.style.cursor = 'pointer';
       } else {
           // Desactivar botones "Nuevo" y "Actualizar"
           boton.disabled = true;
           boton.style.opacity = '0.5';  // Puedes ajustar este valor según lo desees
           boton.style.cursor = 'not-allowed';
       }
   });

   // Desactivar botón "Nuevo"
   const botonNuevo = document.querySelector('.b[value="Nuevo"]');
   botonNuevo.disabled = true;
   botonNuevo.style.opacity = '0.5';  // Puedes ajustar este valor según lo desees
   botonNuevo.style.cursor = 'not-allowed';

   // Verificar y activar el botón de búsqueda usando el estado original
   const btnbuscar = document.getElementById('btnbuscar');
   if (typeof estadoOriginalBtnBuscar !== 'undefined') {
       btnbuscar.disabled = estadoOriginalBtnBuscar;
   }
}


// Función para manejar el clic en el botón "Cancelar" después de editar
document.getElementById('cancelarBtn').addEventListener('click', function() {
   limpiarYDesactivar();
});

function limpiarYDesactivar() {
  console.log('Limpiando y desactivando');
  document.getElementById('txtcodigo').value = "";
  document.getElementById('cbcategoria').value = "";
  document.getElementById('cbmarca').value = "";
  document.getElementById('cbcolor').value = "";
  document.getElementById('cbtalla').value = "";
  document.getElementById('txtdisttintivo').value = "";
  document.getElementById('txtprecioventa').value = "";
  document.getElementById('txtstock').value = "";
  document.getElementById('cbtipobusqueda').value = "";
  document.getElementById('txtbuscar').value = "";
}
// Obtener la tabla por su ID
var tabla = document.getElementById('tablaproducto');

// Asignar el manejador de eventos dblclick a la tabla
tabla.addEventListener('dblclick', manejarDobleClic);


// Desactivar campos de entrada y botones al cargar la interfaz
function desactivarElementos(desactivar) {
   // Desactivar campos de entrada
   const campos = document.querySelectorAll('.t');
   campos.forEach(campo => {
       campo.disabled = desactivar;
   });

   const cbcategoria = document.getElementById('cbcategoria');
   cbcategoria.disabled = desactivar;

   const cbmarca = document.getElementById('cbmarca');
   cbmarca.disabled = desactivar;

   const cbcolor = document.getElementById('cbcolor');
   cbcolor.disabled = desactivar;

   const cbtalla = document.getElementById('cbtalla');
   cbtalla.disabled = desactivar;

   const cbtipobusqueda = document.getElementById('cbtipobusqueda');
   cbtipobusqueda.disabled = desactivar;

   const btnbuscar = document.getElementById('btnbuscar');
   btnbuscar.disabled = desactivar;

   // Desactivar botones (haciéndolos no interactivos y cambiando su estilo), excepto el botón "Nuevo"
   const botones = document.querySelectorAll('.b');
   botones.forEach(boton => {
       boton.disabled = desactivar;
   });
}


document.getElementById('nuevoBtn').addEventListener('click', function () {
  // Llamar a la función para desactivar y activar elementos
  activarNuevo(false);
});

document.getElementById('cancelarBtn').addEventListener('click', function () {
   desactivarTodoExceptoNuevo(true);
});


// Función para manejar el clic en el botón "Cancelar" después de editar
document.getElementById('cancelarBtn').addEventListener('click', function() {
   limpiarYDesactivar();
});










let dataCategorias, dataColores, dataMarcas, dataTallas;

function getDatos() {
    // Realizar solicitudes fetch para obtener datos
    Promise.all([
        fetch('/CtrlCategoria/Get').then(response => response.json()),
        fetch('/CtrlMarca/Get').then(response => response.json()),
        fetch('/CtrlColor/Get').then(response => response.json()),
        fetch('/CtrlTalla/Get').then(response => response.json())
    ])
    .then(([categorias, marcas, colores, tallas]) => {
        // Asignar los datos a las variables globales
        dataCategorias = categorias;
        dataMarcas = marcas;
        dataColores = colores;
        dataTallas = tallas;

        // Llenar los combobox con los datos obtenidos
        fillComboBox('cbcategoria', categorias, 'idCategoria', 'categoria');
        fillComboBox('cbmarca', marcas, 'idMarca', 'marca1');
        fillComboBox('cbcolor', colores, 'idColor', 'color1');
        fillComboBox('cbtalla', tallas, 'idTalla', 'talla1');
    })
    .catch(error => console.error('Error al obtener datos:', error));
}


function fillComboBox(comboBoxId, data, valueField, textField) {
    const comboBox = document.getElementById(comboBoxId);
    // Limpiar combobox
    comboBox.innerHTML = "";

    // Agregar una opción por defecto
    const defaultOption = document.createElement("option");
    defaultOption.text = "Seleccionar...";
    comboBox.add(defaultOption);

    // Agregar opciones con los datos obtenidos
    data.forEach(item => {
        const option = document.createElement("option");
        option.value = item[valueField]; // Utilizar el campo especificado como valor
        option.text = item[textField];   // Utilizar el campo especificado como texto
        comboBox.add(option);
    });
}

function createProducto(e) {
    // Obtener valores de los campos de entrada
    const nombreCategoria = document.getElementById('cbcategoria').value;
    const nombreTalla = document.getElementById('cbtalla').value;
    const nombreColor = document.getElementById('cbcolor').value;
    const nombreMarca = document.getElementById('cbmarca').value;
    const distintivo = document.getElementById('txtdisttintivo').value;
    const precio = document.getElementById('txtprecioventa').value;
    const stock = document.getElementById('txtstock').value;
    const codigo = document.getElementById('txtcodigo').value;

    //VALIDACIONES

    if(codigo ===""){
       alert("Rellene el campo Código");
       e.preventDefault()
       return;
    }

    else if(codigo.length > 6){
       alert("El codigo no puede tener mas de 6 caracteres");
       return;
    }

    if (!nombreCategoria || document.getElementById('cbcategoria').selectedIndex === 0) {
       alert("Seleccione una Categoría");
       e.preventDefault();
       return;
   }
   
   if (!nombreMarca || document.getElementById('cbmarca').selectedIndex === 0) {
       alert("Seleccione una Marca");
       e.preventDefault();
       return;
   }
   
   if (!nombreColor || document.getElementById('cbcolor').selectedIndex === 0) {
       alert("Seleccione un Color");
       e.preventDefault();
       return;
   }
   
   if (!nombreTalla || document.getElementById('cbtalla').selectedIndex === 0) {
       alert("Seleccione una Talla");
       e.preventDefault();
       return;
   }
   
    if(distintivo ===""){
       alert("Rellene el campo Distintivo");
       e.preventDefault()
       return;
    }
    else if(distintivo.length > 15){
       alert("El distintivo no puede tener mas de 15 caracteres");
       return;
    }

    if (!precio || parseFloat(precio) === 0) {
        alert("Ingrese un Precio venta válido");
        e.preventDefault();
        return;
    }
   
    if (!stock || parseFloat(stock) === 0) {
        alert("Ingrese un Stock válido");
        e.preventDefault();
        return;
    }
   

    // Crear un objeto Producto con los valores
    const producto = {
        idCategoria: nombreCategoria,
        idTalla: nombreTalla,
        idColor: nombreColor,
        idMarca: nombreMarca,
        distintivo: distintivo,
        precioVenta: precio,
        stock: stock,
        codProducto: codigo
    };
    console.log(producto);

    // Realizar una solicitud POST para crear un nuevo producto
    fetch('/CtrlProducto/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(producto)
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        // Verificar si la respuesta indica éxito
        if (data === true) {
            console.log('Producto creado exitosamente');
            // Actualizar la tabla con el nuevo producto
            updateTable();
        } else {
            console.error('Error al crear el producto');
        }
    })
    .catch(error => console.error('Error:', error));
    limpiarYDesactivar();
    desactivarTodoExceptoNuevo();
}


var id;
function updateprod() {
   const cod = document.getElementById('txtcodigo').value;
   const categoria = document.getElementById('cbcategoria').value;
   const marc = document.getElementById('cbmarca').value;
   const color = document.getElementById('cbcolor').value;
   const talla = document.getElementById('cbtalla').value;
   const distintivo = document.getElementById('txtdisttintivo').value;
   const precio = document.getElementById('txtprecioventa').value;
   const stock = document.getElementById('txtstock').value;

   const producto = {
       idProducto: id,
       idCategoria: categoria,
       idTalla: talla,
       idColor: color,
       idMarca: marc,
       distintivo: distintivo,
       precioVenta: precio,
       stock: stock,
       codProducto: cod
   };
   fetch(`http://localhost:5282/CtrlProducto/Update?code=${id}`,
   {
       method: 'PUT',
       headers: {
           'Content-Type': 'application/json'
       },
       body: JSON.stringify(producto)
   })
   .then(response => response.json())
   .then(data => {
       console.log(data);
       // Verificar si la respuesta indica éxito
       if (data === true) {
           updateTable();
           console.log('Marca actualizada exitosamente');
       }
       else
       {
           console.error('Error al actualizar la marca');
       }
   })
   .catch(error => console.error('Error:', error));
   }


   function removeUser(id) {
    fetch(`/CtrlProducto/Remove/${id}`, {
        method: 'DELETE'
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        console.log(data);
        // Verificar si la respuesta indica éxito
        if (data === true) {
            console.log('Producto eliminado exitosamente');
            updateTable();
        } else {
            console.error('Error al eliminar el Producto');
        }
    })
    .catch(error => console.error('Error en la solicitud:', error.message));
    limpiarYDesactivar();
}



// Función para manejar el evento dblclick
function manejarDobleClic(event)
    {
        if (event.target.textContent.toLowerCase() === 'eliminar') 
        {
            var fila = event.target.parentNode;
            id = fila.cells[0].innerText;

            var confirmacion = window.confirm('¿Estás seguro de que deseas eliminar este Producto?');

             // Si el usuario hace clic en "Aceptar" en el cuadro de diálogo de confirmación, eliminar la marca
            if (confirmacion) {
                removeUser(id);
            }
        }
       
        if (event.target.textContent.toLowerCase() === 'editar') 
        {
            var fila = event.target.parentNode;
            id = fila.cells[0].innerText;

            document.getElementById('txtcodigo').value=fila.cells[1].innerText;
            document.getElementById('cbcategoria').value=fila.cells[2].innerText;
            document.getElementById('cbmarca').value=fila.cells[3].innerText;
            document.getElementById('cbcolor').value=fila.cells[4].innerText;
            document.getElementById('cbtalla').value=fila.cells[5].innerText;
            document.getElementById('txtdisttintivo').value=fila.cells[6].innerText;
            document.getElementById('txtprecioventa').value=fila.cells[7].innerText;
            document.getElementById('txtstock').value=fila.cells[8].innerText;
            console.log(id);

            // Habilitar campos de entrada Cliente
            document.getElementById('txtcodigo').disabled = false;
            document.getElementById('cbcategoria').disabled = false;
            document.getElementById('cbmarca').disabled = false;
            document.getElementById('cbcolor').disabled = false;
            document.getElementById('cbtalla').disabled = false;
            document.getElementById('txtdisttintivo').disabled = false;
            document.getElementById('txtprecioventa').disabled = false;
            document.getElementById('txtstock').disabled = false;
            document.getElementById('cbtipobusqueda').disabled = false;
            document.getElementById('txtbuscar').disabled = false;

            // Desactivar botones "Nuevo" y "Guardar" Cliente
            document.getElementById('nuevoBtn').disabled = true;
            document.getElementById('nuevoBtn').style.opacity = '0.5';
            document.getElementById('nuevoBtn').style.cursor = 'not-allowed';

            document.getElementById('guardarBtn').disabled = true;

            // Activar botones "Actualizar" y "Cancelar" Cliente
            document.getElementById('actualizarBtn').disabled = false;
            document.getElementById('actualizarBtn').style.opacity = '1';
            document.getElementById('actualizarBtn').style.cursor = 'pointer';

            document.getElementById('cancelarBtn').disabled = false;
            document.getElementById('cancelarBtn').style.opacity = '1';
            document.getElementById('cancelarBtn').style.cursor = 'pointer';
        }
    }

    document.getElementById('actualizarBtn').addEventListener('click', function() {
        limpiarYDesactivar();
        desactivarTodoExceptoNuevo();
    });

    // Obtener la tabla por su ID
    var tabla = document.getElementById('tablaproducto');

    // Asignar el manejador de eventos dblclick a la tabla
    tabla.addEventListener('dblclick', manejarDobleClic);

// Actualizar la tabla 
function updateTable() {
    // Realizar una solicitud GET para obtener los datos de producto
    fetch('/CtrlProducto/Get')
        .then(response => response.json())
        .then(data => {
            console.log(data);

            // Obtener la referencia a la tabla
            const tableBody = document.querySelector('.conteyner tbody');

            // Limpiar el contenido actual de la tabla
            tableBody.innerHTML = '';

       // Iterar sobre los datos y agregar filas a la tabla
       data.forEach(producto => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${producto.idProducto}</td>
                    <td>${producto.codProducto}</td>
                    <td>${producto.idCategoria}</td>
                    <td>${producto.idMarca}</td>
                    <td>${producto.idColor}</td>
                    <td>${producto.idTalla}</td>
                    <td>${producto.distintivo}</td>
                    <td>${producto.precioVenta}</td>
                    <td>${producto.stock}</td>
                    <td>Editar</td>
                    <td>Eliminar</td>
                `;
                tableBody.appendChild(row);
        });
    })
    .catch(error => console.error('Error:', error));
}

window.onload = function() {
    desactivarTodoExceptoNuevo();
    getDatos();
    updateTable();
    limpiarYDesactivar();
};