export const months = [
  { name: 'january' },
  { name: 'february' },
  { name: 'march' },
  { name: 'april' },
  { name: 'may' },
  { name: 'june' },
  { name: 'july' },
  { name: 'august' },
  { name: 'september' },
  { name: 'october' },
  { name: 'november' },
  { name: 'december' }
]

export const monthCategories = months.map((month) => month.name.slice(0, 3).toUpperCase())
