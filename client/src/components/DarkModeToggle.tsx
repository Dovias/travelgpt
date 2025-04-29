import { useState } from "react";

function DarkModeToggle() {
  const [darkMode, setDarkMode] = useState(false);
  const toggleDarkMode = () => {
    const html = document.documentElement;
    html.classList.toggle("dark");
    setDarkMode(!darkMode);
  };

    return (
        <button onClick={toggleDarkMode} className="px-2 py-1 text-sm bg-gray-600 rounded hover:bg-gray-500">
          {darkMode ? "☀️" : "🌙"}
        </button>
    );
}

export default DarkModeToggle;