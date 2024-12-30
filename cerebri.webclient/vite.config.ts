import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(), tsconfigPaths()],
    css: {
        postcss: './postcss.config.cjs',
    },
    resolve: {
        alias: {
            components: '/src/components',
            pages: '/src/pages',
        },
    },
    server: {
        port: 53767,
    }
})
