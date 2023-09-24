import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import laravel from 'laravel-vite-plugin';
import { mkdirSync } from 'fs';

const outDir = '../wwwroot/dist';

mkdirSync(outDir, { recursive: true });

// https://vitejs.dev/config/
export default defineConfig(({ ssrBuild }) => {
  return {
    plugins: [
      laravel({
        input: ['src/main.tsx'],
        publicDirectory: outDir,
        refresh: true,
        ssr: ssrBuild ? ['src/ssr.tsx'] : undefined,
        ssrOutputDirectory: '../wwwroot/ssr',
      }),
      react(),
    ],
    build: {
      outDir,
      emptyOutDir: true,
    },
  };
});
