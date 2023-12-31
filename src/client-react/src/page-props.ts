import { Toast } from './models/toast';
import { User } from './models/user';

export type PageProps<T extends Record<string, unknown> = Record<string, unknown>> = T & {
  auth: {
    user: User;
  };
  toast: Toast;
};
