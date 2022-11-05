import { ReactNode } from 'react';

import FoodBankTwoToneIcon from '@mui/icons-material/FoodBankTwoTone';
import StraightenTwoToneIcon from '@mui/icons-material/StraightenTwoTone';
import MenuBookTwoToneIcon from '@mui/icons-material/MenuBookTwoTone';
import ShoppingCartTwoToneIcon from '@mui/icons-material/ShoppingCartTwoTone';
import BrightnessLowTwoToneIcon from '@mui/icons-material/BrightnessLowTwoTone';
import FlatwareTwoToneIcon from '@mui/icons-material/FlatwareTwoTone';

export interface MenuItem {
  link?: string;
  icon?: ReactNode;
  badge?: string;
  items?: MenuItem[];
  name: string;
}

export interface MenuItems {
  items: MenuItem[];
  heading: string;
}

const menuItems: MenuItems[] = [
  {
    heading: 'Dashboard',
    items: [
      {
        name: 'Main',
        link: '/dashboard/main',
        icon: BrightnessLowTwoToneIcon
      }
    ]
  },
  {
    heading: 'Zarządzanie produktami',
    items: [
      {
        name: 'Produkty',
        link: '/product/actions',
        icon: ShoppingCartTwoToneIcon
      },
      {
        name: 'Kategorie',
        link: '/product/category',
        icon: MenuBookTwoToneIcon
      },
      {
        name: 'Miary',
        link: '/product/measure',
        icon: StraightenTwoToneIcon
      }
    ]
  },
  {
    heading: 'Zarządzanie przepisami',
    items: [
      {
        name: 'Dodaj przepis',
        link: '/recipe/add',
        icon: FlatwareTwoToneIcon,
        items: [
          {
            name: 'Podstawowe informacje',
            link: '/recipe/add/info'
          },
          {
            name: 'Składniki',
            link: '/recipe/add/ingredients'
          },
          {
            name: 'Opis',
            link: '/recipe/add/description'
          }
        ]
      },
      {
        name: 'Lista przepisów',
        link: '/recipe/list',
        icon: FoodBankTwoToneIcon
      }
    ]
  }
];

export default menuItems;
