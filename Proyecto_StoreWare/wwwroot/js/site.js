$(document).ready(function () {
    // Cargar productos al iniciar
    loadProducts();

    // Manejar el envío del formulario
    $('#product-form').submit(function (e) {
        e.preventDefault();

        const product = {
            id: $('#product-id').val() || 0,
            nombre: $('#nombre').val(),
            precio: parseFloat($('#precio').val()),
            stock: parseInt($('#stock').val())
        };

        if (product.id === 0) {
            // Crear nuevo producto
            createProduct(product);
        } else {
            // Actualizar producto existente
            updateProduct(product);
        }
    });

    // Manejar cancelar edición
    $('#cancel-edit').click(function () {
        resetForm();
    });
});

// Cargar todos los productos
function loadProducts() {
    $.ajax({
        url: '/api/Producto',
        type: 'GET',
        success: function (products) {
            const tableBody = $('#product-table');
            tableBody.empty();

            products.forEach(function (product) {
                tableBody.append(`
                    <tr>
                        <td>${product.id}</td>
                        <td>${product.nombre}</td>
                        <td>$${product.precio.toFixed(2)}</td>
                        <td>${product.stock}</td>
                        <td>
                            <button class="btn btn-sm btn-warning edit-product" data-id="${product.id}">Editar</button>
                            <button class="btn btn-sm btn-danger delete-product" data-id="${product.id}">Eliminar</button>
                        </td>
                    </tr>
                `);
            });

            // Agregar eventos a los botones
            $('.edit-product').click(function () {
                const productId = $(this).data('id');
                editProduct(productId);
            });

            $('.delete-product').click(function () {
                const productId = $(this).data('id');
                deleteProduct(productId);
            });
        },
        error: function (error) {
            console.error('Error al cargar productos:', error);
            alert('Error al cargar productos');
        }
    });
}

// Crear un nuevo producto
function createProduct(product) {
    $.ajax({
        url: '/api/Producto',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(product),
        success: function () {
            alert('Producto creado con éxito');
            resetForm();
            loadProducts();
        },
        error: function (error) {
            console.error('Error al crear producto:', error);
            alert('Error al crear producto');
        }
    });
}

// Actualizar un producto existente
function updateProduct(product) {
    $.ajax({
        url: `/api/Producto/${product.id}`,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(product),
        success: function () {
            alert('Producto actualizado con éxito');
            resetForm();
            loadProducts();
        },
        error: function (error) {
            console.error('Error al actualizar producto:', error);
            alert('Error al actualizar producto');
        }
    });
}

// Editar un producto (cargar datos en el formulario)
function editProduct(id) {
    $.ajax({
        url: `/api/Producto/${id}`,
        type: 'GET',
        success: function (product) {
            $('#product-id').val(product.id);
            $('#nombre').val(product.nombre);
            $('#precio').val(product.precio);
            $('#stock').val(product.stock);

            $('#form-title').text('Editar Producto');
            $('#cancel-edit').show();
        },
        error: function (error) {
            console.error('Error al cargar producto:', error);
            alert('Error al cargar producto para editar');
        }
    });
}

// Eliminar un producto
function deleteProduct(id) {
    if (confirm('¿Estás seguro de que quieres eliminar este producto?')) {
        $.ajax({
            url: `/api/Producto/${id}`,
            type: 'DELETE',
            success: function () {
                alert('Producto eliminado con éxito');
                loadProducts();
            },
            error: function (error) {
                console.error('Error al eliminar producto:', error);
                alert('Error al eliminar producto');
            }
        });
    }
}

// Resetear el formulario
function resetForm() {
    $('#product-id').val('');
    $('#product-form')[0].reset();
    $('#form-title').text('Agregar Nuevo Producto');
    $('#cancel-edit').hide();
}