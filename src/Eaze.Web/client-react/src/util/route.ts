export const isCurrentRoute = (path: string, ignoreParams: boolean = false) => {
  if (ignoreParams) {
    return window.location.pathname.startsWith(path);
  }

  return window.location.pathname === path;
};
