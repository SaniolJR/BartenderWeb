import type { Dispatch, SetStateAction, ChangeEvent } from 'react'
import { TextField } from '@mui/material'
//file with function that displays component where user can type
//  maximum acceptable number of missin ingredients
//if means that Backend will return all drinks with user have 100% ingredients +
//all drinks that have more than that number missing ones

//arguments: start number, setter for parent usestate

type Props = {
  number: number
  setNumber: Dispatch<SetStateAction<number>>
}

//function
export default function MissingIngredientCount({ number, setNumber }: Props) {
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const res = e.target.value

    if (res === '') {
      setNumber(0)
      return
    }

    setNumber(Number(res))
  }

  return (
    <TextField
      label="Max number of missing ingredients"
      type="number"
      value={number}
      onChange={handleChange}
      inputProps={{ min: 0 }}
      size="small"
      //size of arrows to decrease and incerase num by one
      sx={{
    '& input[type=number]::-webkit-inner-spin-button, & input[type=number]::-webkit-outer-spin-button':
        {
        transform: 'scale(1.6)',
        transformOrigin: 'right center',
        },
    }}
    />
  )
}