export interface NavData {
  name?: string;
  url?: string;
  icon?: string;
  badge?: any;
  title?: boolean;
  children?: any;
  variant?: string;
  attributes?: object;
  divider?: boolean;
  class?: string;
}

export const navItems: NavData[] = [
  {
    name: 'Home',
    url: '/home'
  },
  {
    name: 'Software Houses',
    url: '/sh'
  },
  {
    name: 'Agenda',
    url: '/agenda'
  },
  {
    name: 'Homologacão',
    url: '/homologacao'
  },
  {
    name: 'Usuários',
    url: '/usuario'
  },
  {
    name: 'Perguntas e espostas',
    url: '/perguntaresposta'
  }
];
