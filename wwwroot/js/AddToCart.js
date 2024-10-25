let cart = JSON.parse(localStorage.getItem('cart')) || [];

function updateCartDisplay() {
    const cartContent = document.querySelector('.cart-content');
    const cartCount = document.querySelector('.cart-count');
    const totalPriceDisplay = document.querySelector('.total-price');
    const cartEmptyMessage = document.querySelector('.cart-empty');

    cartContent.innerHTML = '';
    cartCount.textContent = cart.length;

    if (cart.length === 0) {
        cartEmptyMessage.classList.remove('d-none');
        totalPriceDisplay.textContent = '0';
        return;
    } else {
        cartEmptyMessage.classList.add('d-none');
    }

    cart.forEach((item, index) => {
        const cartItem = document.createElement('div');
        cartItem.className = 'cart-item col d-flex pt-3 ps-2 pb-3 mb-3 border border-1';
        cartItem.innerHTML = `
            <img src="${item.img}" style="width: 70px; height: 70px;" />
            <div class="cart-detail w-100 ps-2 text-start">
                <h6 class="cart-name font-serif text-black">${item.name}</h6>
                <div class="d-flex justify-content-between align-items-center">
                    <div class="quantity input-group" style="width: 140px;">
                        <button class="quantity-btn-left btn-outline-secondary border-dark border border-end-0" type="button" aria-label="Giảm số lượng">−</button>
                        <input type="number" class="form-control text-center border-top border-bottom border-0 border-dark bg-white" id="quantityInput${index}" value="${item.quantity}" min="1" max="10" data-price="${item.price}" readonly>
                        <button class="quantity-btn-right btn-outline-secondary border-dark border border-start-0" type="button" aria-label="Tăng số lượng">+</button>
                    </div>
                    <div class="font-14 text-center fw-500">
                        <span style="color: #1250dc;" id="priceDisplay${index}">${(item.price * item.quantity).toLocaleString('vi-VN')}đ</span>
                    </div>
                </div>
            </div>
            <button class="close-cart border-0 bg-white" style="height: fit-content; margin-top: -16px;" data-index="${index}">
                <i class="fa-solid fa-xmark" style="font-size: 12px;" aria-hidden="true"></i>
            </button>
        `;

        cartContent.appendChild(cartItem);

        // Xóa sản phẩm khỏi giỏ hàng
        const removeButton = cartItem.querySelector('.close-cart');
        removeButton.addEventListener('click', function () {
            cart.splice(index, 1);
            updateCartDisplay();
            localStorage.setItem('cart', JSON.stringify(cart));
        });

        // Nút tăng số lượng
        const btnIncrease = cartItem.querySelector('.quantity-btn-right');
        btnIncrease.addEventListener('click', function () {
            if (item.quantity < 10) { // Giới hạn tối đa 10
                item.quantity += 1;
                updateQuantityAndPrice(index, item.quantity, item.price);
            }
        });

        // Nút giảm số lượng
        const btnDecrease = cartItem.querySelector('.quantity-btn-left');
        btnDecrease.addEventListener('click', function () {
            if (item.quantity > 1) { // Giới hạn tối thiểu 1
                item.quantity -= 1;
                updateQuantityAndPrice(index, item.quantity, item.price);
            }
        });

        // Vô hiệu hóa nút giảm nếu số lượng là 1
        if (item.quantity === 1) {
            btnDecrease.disabled = true;
        } else {
            btnDecrease.disabled = false;
        }
    });

    updateTotalPrice();
}

// Cập nhật số lượng và giá sản phẩm
function updateQuantityAndPrice(index, quantity, price) {
    const quantityInput = document.getElementById(`quantityInput${index}`);
    const priceDisplay = document.getElementById(`priceDisplay${index}`);

    quantityInput.value = quantity;
    priceDisplay.textContent = (price * quantity).toLocaleString('vi-VN') + 'đ';

    updateTotalPrice();
    localStorage.setItem('cart', JSON.stringify(cart));
}

// Cập nhật tổng tiền
function updateTotalPrice() {
    let totalPrice = 0;
    cart.forEach(item => {
        totalPrice += item.price * item.quantity;
    });
    const totalPriceDisplay = document.querySelector('.total-price');
    totalPriceDisplay.textContent = totalPrice.toLocaleString('vi-VN') + 'đ';
}

//Xử lý nút thêm sp
document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', function () {
        const productId = this.getAttribute('data-id');
        const productName = this.getAttribute('data-name').trim();
        const productPrice = parseInt(this.getAttribute('data-price'));
        const productImg = this.getAttribute('data-img').trim();

        const quantityInput = document.querySelector('#data-quantity');
        let quantity = 1;

        if (quantityInput) {
            const inputValue = parseInt(quantityInput.value);
            if (!isNaN(inputValue) && inputValue > 0) {
                quantity = inputValue; 
            }
        }

        const product = {
            id: productId,
            name: productName,
            price: productPrice,
            img: productImg,
            quantity: quantity
        };

        const existingProductIndex = cart.findIndex(item => item.id === product.id);

        if (existingProductIndex >= 0) {
            cart[existingProductIndex].quantity += 1;
        } else {
            cart.push(product);
        }

        localStorage.setItem('cart', JSON.stringify(cart));

        showAlert(`Đã thêm ${productName} vào giỏ hàng`);

        updateCartDisplay();
    });
});

function showAlert(message) {
    const alertDiv = document.createElement('div');
    alertDiv.textContent = message;
    alertDiv.style.position = 'fixed';
    alertDiv.style.top = '20px';
    alertDiv.style.right = '20px';
    alertDiv.style.backgroundColor = '#1250DC';
    alertDiv.style.color = 'white';
    alertDiv.style.padding = '10px';
    alertDiv.style.borderRadius = '5px';
    document.body.appendChild(alertDiv);

    setTimeout(function () {
        alertDiv.remove();
    }, 3000);
}

document.addEventListener('DOMContentLoaded', function () {
    updateCartDisplay();
});

