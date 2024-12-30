/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        blizzardBlue: '#a8dbeb',
        marzipan: '#f9e79f',
        bittersweet: '#ff6e61',
        seaPink: '#f0a3a5',
        pearlLusta: '#fcf2d9',
        astra: '#fbeac6',
        loblolly: '#c3c9ca',
      }
    },
  },
  plugins: [
    require('tailwind-scrollbar'),
  ],
}

