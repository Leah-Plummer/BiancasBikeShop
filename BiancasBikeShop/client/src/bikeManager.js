const apiUrl = '/api/bike';

export const getBikes = () =>  {
    return fetch(`${apiUrl}/GetAllBikes`)
      .then((res) => res.json())
  };
      
    

// export const getBikeById = (id) => {
//     //add implementation here... 
// }

// export const getBikesInShopCount = () => {
//     //add implementation here... 
// }