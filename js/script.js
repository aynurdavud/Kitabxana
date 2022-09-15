
window.addEventListener('click', function (event) {
	if (event.target.hasAttribute('data-cart')) {
		const card = event.target.closest('.book_cart_parent');
		const productInfo = {
			id: card.dataset.id,
			imgSrc: card.querySelector('.photo_aftor').getAttribute('src'),
			title: card.querySelector('.book_opis_text').innerText,
			aftor: card.querySelector('.aftor_').innerText,
			name: card.querySelector('.name_').innerText,
		};
		console.log(productInfo)

		const cartItemHTML = `<div data-id="${productInfo.id}" class="book_cart_parent">
					<div class="book_cart">
						<img class="photo_aftor" src="${productInfo.imgSrc}"></img>
						<div class="aftor_text">
							<p class="aftor_">${productInfo.aftor}</p>
							<br>
							<p class="name_">${productInfo.name}</p>
						</div>
						<div class="book_opis_block">
							<p class="book_opis_text">${productInfo.title}</p>
						</div>
					</div>
					<button data-card class="book_button4"></button>
				</div>`;
		localStorage.setItem(productInfo.id, cartItemHTML);
    	window.location = "index3.html";
	}
	if (event.target.hasAttribute('data-card')) {
		const card = event.target.closest('.book_cart_parent');
		const productInfo = {
			id: card.dataset.id,
			imgSrc: card.querySelector('.photo_aftor').getAttribute('src'),
			title: card.querySelector('.book_opis_text').innerText,
			aftor: card.querySelector('.aftor_').innerText,
			name: card.querySelector('.name_').innerText,
		};
		console.log(productInfo);
		localStorage.removeItem(productInfo.id);
		location.reload();
	}
})