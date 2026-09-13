const contactForm = document.getElementById("contactForm");

if (contactForm) {
    contactForm.addEventListener("submit", (event) => {
        event.preventDefault();

        if (!contactForm.checkValidity()) {
            contactForm.reportValidity();
            return;
        }

        document.getElementById("contactStatus").textContent = "Thanks for contacting us. Our team will get back to you soon.";
        contactForm.reset();
    });
}
