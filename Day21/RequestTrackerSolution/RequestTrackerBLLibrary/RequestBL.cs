﻿using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class RequestBL : IRequestBL
    {
        private readonly IRepository<int, Request> _repository;
        public RequestBL()
        {
            IRepository<int, Request> repo = new RequestRepository(new RequestTrackerContext());
            _repository = repo;
        }

        

        public async Task<int> OpenRequest(Request request)
        {
            request.RequestStatus = "Open";
            var result = await _repository.Add(request);
            return result.RequestNumber;
        }

        public async Task<Request> GetRequestById(int RequestId)
        {
            Request request = await _repository.Get(RequestId);
            return request;
        }

        public async Task<IList<Request>> GetAllRequests()
        {
            IList<Request> requests = await _repository.GetAll();
            if (requests.Count == 0)
            {
                return null;
            }
            return requests;
        }

        public async Task<bool> CloseRequest(int RequestId, Employee employee)
        {
            Request validRequest = await _repository.Get(RequestId);
            if (validRequest == null)
            {
                return false;
            }
            validRequest.ClosedDate = DateTime.Now;
            validRequest.RequestClosedBy = employee.Id;
            validRequest.RequestStatus = "Closed";
            await _repository.Update(validRequest);
            return true;
        }

        public async Task<bool> UpdateRequest(int RequestId, string status)
        {
            Request validRequest = await _repository.Get(RequestId);
            if (validRequest == null)
            {
                return false;
            }
            validRequest.RequestStatus = status;
            await _repository.Update(validRequest);
            return true;
        }
    }
}
