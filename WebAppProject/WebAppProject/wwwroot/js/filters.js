document.addEventListener("DOMContentLoaded", function () {
    console.log("DOMContentLoaded event fired");

    // Flaga sprawdzająca, czy resetować ustawienia checkboxów 
    let resetCheckboxes = true;

    // Obsługa rozwinięcia filtra
    var toggleFilters = document.getElementById("toggleFilters");
    if (toggleFilters) {
        toggleFilters.addEventListener("click", function () {
            var filters = document.getElementById("filtersSection");
            if (filters.style.display === "none" || filters.style.display === "") {
                filters.style.display = "block";
                this.textContent = "-";
            } else {
                filters.style.display = "none";
                this.textContent = "+";
            }
        });
    } else {
        console.error("Element toggleFilters not found");
    }

    // Dodaj zdarzenie kliknięcia dla przycisku zastosowania filtrów 
    var applyFiltersButton = document.getElementById("applyFiltersButton");
    if (applyFiltersButton) {
        applyFiltersButton.addEventListener("click", function () {
            // Nie resetuj checkboxów podczas stosowania filtrów 
            resetCheckboxes = false;
        });
    } else { console.error("Element applyFiltersButton not found"); }

    // Funkcja do obsługi checkboxów "All"
    function setupSelectAll(sectionClass, allCheckboxId) {
        const allCheckbox = document.getElementById(allCheckboxId);
        if (!allCheckbox) {
            console.error(`Element with id ${allCheckboxId} not found`);
            return;
        }
        const sectionCheckboxes = document.querySelectorAll(`.${sectionClass}`);
        if (!sectionCheckboxes.length) {
            console.error(`No elements found with class ${sectionClass}`);
            return;
        }

        // Przywracanie stanu checkboxów z localStorage
        sectionCheckboxes.forEach(checkbox => {
            const storedValue = localStorage.getItem(checkbox.id);
            checkbox.checked = storedValue !== null ? JSON.parse(storedValue) : true;
        });
        const allStoredValue = localStorage.getItem(allCheckboxId);
        allCheckbox.checked = allStoredValue !== null ? JSON.parse(allStoredValue) : true;

        // Zaznaczanie/Odznaczanie wszystkich checkboxów w sekcji
        allCheckbox.addEventListener("change", function () {
            const isChecked = this.checked;
            sectionCheckboxes.forEach(checkbox => {
                checkbox.checked = isChecked;
                localStorage.setItem(checkbox.id, isChecked);
            });
            localStorage.setItem(allCheckboxId, isChecked);
        });

        // Jeśli którykolwiek checkbox jest odznaczony, odznacz "All"
        sectionCheckboxes.forEach(checkbox => {
            checkbox.addEventListener("change", function () {
                if (!this.checked) {
                    allCheckbox.checked = false;
                }
                // Jeśli wszystkie są zaznaczone, zaznacz "All"
                else if ([...sectionCheckboxes].every(cb => cb.checked)) {
                    allCheckbox.checked = true;
                }
                localStorage.setItem(checkbox.id, this.checked);
            });
        });
    }

    // Inicjalizacja checkboxów "All" dla każdej sekcji
    setupSelectAll("genre-checkbox", "selectAllGenres");
    setupSelectAll("category-checkbox", "selectAllCategories");
    setupSelectAll("author-checkbox", "selectAllAuthors");
    setupSelectAll("publisher-checkbox", "selectAllPublishers");
    setupSelectAll("language-checkbox", "selectAllLanguages");

    // Dynamiczna walidacja dat
    const startDateInput = document.getElementById("StartDate");
    const endDateInput = document.getElementById("EndDate");
    const dateError = document.getElementById("dateError");

    if (startDateInput && endDateInput && dateError) {
        startDateInput.addEventListener("change", validateDates);
        endDateInput.addEventListener("change", validateDates);
    }
    function validateDates() {
        const startDate = new Date(startDateInput.value);
        const endDate = new Date(endDateInput.value);

        if (startDate && endDate && startDate > endDate) {
            dateError.style.display = "block";
        } else {
            dateError.style.display = "none";
        }
    }

    // Walidacja daty końcowej >= daty początkowej
    const form = document.querySelector("form");
    if (form) {
        form.addEventListener("submit", function (e) {
            const startDate = new Date(document.getElementById("StartDate").value);
            const endDate = new Date(document.getElementById("EndDate").value);

            if (startDate && endDate && startDate > endDate) {
                e.preventDefault(); // Zatrzymaj wysyłanie formularza
                alert("End Date must be greater than or equal to Start Date.");
            }
        });
    }

    // Resetowanie checkboxów podczas zmiany strony 
    const paginationLinks = document.querySelectorAll(".page-link");
    paginationLinks.forEach(link => {
        link.addEventListener("click", function () {
            // Nie resetuj checkboxów podczas paginacji 
            resetCheckboxes = false;
        });
    });

    // Resetowanie checkboxów przy innych akcjach nawigacyjnych 
    window.addEventListener("beforeunload", function (event) {
        // Zresetuj checkboxy tylko podczas zmiany strony (nawigacji poza bieżącą stronę) 
        if (resetCheckboxes) {
            localStorage.clear();
        }
    });
});
