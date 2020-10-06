﻿using System;
using System.Numerics;
namespace Server
{
    public class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        float moveSpeed = 5f / Constants.TICKS_PER_SECOND;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            inputs = new bool[4];
        }

        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                _inputDirection.Y += 1;   //W
            }
            if (inputs[1])
            {
                _inputDirection.X -= 1;    //A
            }
            if (inputs[2])
            {
                _inputDirection.Y -= 1;    //S
            }
            if (inputs[3])
            {
                _inputDirection.X += 1;    //D
            }

            Move(_inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            Vector3 _forward = Vector3.Transform(new Vector3(0, 1, 0), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 0, 1)));

            Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;

            position += _moveDirection * moveSpeed;
            
            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
            
        }

        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }

    }
}
