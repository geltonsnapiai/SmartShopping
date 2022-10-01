import React, { Component } from 'react';
import SearchBar from './SearchBar/SearchBar';

export class Search extends Component {

    render() {
        return(
            <div>
                <div> 
                    <SearchBar placeHolder="Enter product"/>    
                </div>
               
            </div>
        )
    }
}