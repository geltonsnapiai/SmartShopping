import React from 'react'
import "./searchBar.css"
import SearchButton from "../SearchButton/SearchButton"

function SearchBar({placeHolder, data}) {
  return (
    <div className="SearchBar">
        <div className="search-input"> 
            <input type="text" className="search-input"  placeHolder={placeHolder}/>

            <span>
              <button type="submit"> 
                <i className="fa fa-search"></i>
              </button>
            </span>
            
        </div>
        <hr className="hr"/>

        <div className="dataResult"></div>
    </div>
  )
}

export default SearchBar